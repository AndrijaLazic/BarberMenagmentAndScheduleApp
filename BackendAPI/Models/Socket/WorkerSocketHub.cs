using BackendAPI.Models.Database;
using BackendAPI.Services.DataService;
using BackendAPI.Services.WorkerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Primitives;
using System.Text.Json;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BackendAPI.Models.Socket
{
    public class WorkerSocketHub : Hub<IWorkerChatHub>
    {
        public readonly SharedDB _sharedDb;
        public readonly IWorkerService _workerService;

        public WorkerSocketHub(SharedDB sharedDb, IWorkerService workerService)
        {
            _sharedDb = sharedDb;
            _workerService = workerService;
        }

        public override async Task OnConnectedAsync()
        {
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string ?workerID=_sharedDb.disconnectWorker(Context.ConnectionId);
            if(workerID != null)
            {
                await Clients.All.DisconnectedFromAppMessage(workerID);
            }
            
        }

        /// <summary>
        ///     Join a server to receive notifications
        /// </summary>
        /// <param name="JWT">Your valid JWT</param>
        /// <returns></returns>
        public async Task JoinServer()
        {
            // Access the HTTP context
            HttpContext? httpContext = Context.GetHttpContext();

            string JWT = "";
            if (httpContext == null)
            {
                await Clients.Caller.ValidationError("InvalidJWT");
                return;
            }
            if (!httpContext!.Request.Headers.TryGetValue("JWT", out StringValues header))
            {
                await Clients.Caller.ValidationError("InvalidJWT");
                return;
            }

            string ?userId=WorkerService.ValidateToken(JWT);

            if(userId == null)
            {
                await Clients.Caller.ValidationError("InvalidJWT");
                return;
            }

            _sharedDb.setWorkerOnline(userId, Context.ConnectionId);
            await Clients.All.JoinedServerMessage(userId);
        }


        /// <summary>
        ///     Creates a chat room with specified user
        /// </summary>
        /// <param name="user1Id">User that wants to connect</param>
        /// <param name="user2Id">User you want to connect with</param>
        /// <returns></returns>
        public async Task JoinChatWithUser(int user1Id, int user2Id)
        {
            // Access the HTTP context
            HttpContext ?httpContext = Context.GetHttpContext();

            string JWT="";
            if (httpContext == null)
            {
                await Clients.Caller.ValidationError("InvalidJWT");
                return;
            }
            if (!httpContext!.Request.Headers.TryGetValue("JWT", out StringValues header))
            {
                await Clients.Caller.ValidationError("InvalidJWT");
                return;
            }
            JWT = header.ToString();

            string? userId = WorkerService.ValidateToken(JWT);
            if (userId == null)
            {
                await Clients.Caller.ValidationError("InvalidJWT");
                return;
            }

            WorkerCommunication? oldChat;
            try
            {
                oldChat = (await _workerService.GetChat(int.Parse(userId!), user2Id)).Data;
                if (oldChat == null)
                {
                    oldChat = (await _workerService.CreateWorkerChat(int.Parse(userId), user2Id)).Data;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("JWTNotValid"))
                {
                    await Clients.Caller.ValidationError("InvalidJWT");
                    return;
                }
                Console.WriteLine(ex);
                return;
            }
            WorkerChat chat = _sharedDb.AddWorkerChat(user1Id, user2Id, Context.ConnectionId, oldChat!.Id);

            await Groups.AddToGroupAsync(Context.ConnectionId, chat.chatKey);

            await Clients.Group(chat.chatKey).JoinedMessage(JsonSerializer.Serialize(new
            {
                UserJoined = user1Id
            }));
        }

        public async Task SendMessage(int user1Id, int user2Id, string message)
        {
            WorkerChat ?chat=_sharedDb.GiveWorkerChat(user1Id, user2Id);
            if (chat == null)
                return;

            await Clients.Group(chat.chatKey)
                    .ReceiveSpecificMessage(user2Id, message);
        }
    }

}
