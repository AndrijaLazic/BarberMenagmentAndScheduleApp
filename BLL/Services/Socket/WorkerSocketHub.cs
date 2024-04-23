using BLL.Services.DataService;
using Domain.Models.Database;
using Domain.Models.Socket;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Primitives;
using System.Text.Json;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BLL.Services.Socket
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
            string? workerID = _sharedDb.disconnectWorker(Context.ConnectionId);
            if (workerID != null)
            {
                await Clients.All.DisconnectedFromAppMessage(workerID);
            }

        }

        /// <summary>
        ///     Join a server to receive notifications. You need to send valid JWT
        /// </summary>
        /// <param name="JWT">Your valid JWT</param>
        /// <returns></returns>
        public async Task JoinServer(string JWT)
        {

            string? userId = WorkerService.ValidateToken(JWT);

            if (userId == null)
            {
                await Clients.Caller.ValidationError("InvalidJWT");
                return;
            }
            Context.Items["JWTtoken"] = JWT;


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


            string JWT = "";
            if (Context.Items["JWTtoken"] != null)
                JWT = Context.Items["JWTtoken"]!.ToString()!;
            if (JWT == "")
            {
                await Clients.Caller.ValidationError("InvalidJWT");
                return;
            }

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

        public async Task SendMessage(int user2Id, string message)
        {
            string JWT = "";
            if (Context.Items["JWTtoken"] != null)
                JWT = Context.Items["JWTtoken"]!.ToString()!;
            if (JWT == "")
            {
                await Clients.Caller.ValidationError("InvalidJWT");
                return;
            }

            string? userId = WorkerService.ValidateToken(JWT);
            if (userId == null)
            {
                await Clients.Caller.ValidationError("InvalidJWT");
                return;
            }

            WorkerChat? chat = _sharedDb.GiveWorkerChat(int.Parse(userId), user2Id);
            if (chat == null)
                return;

            await _workerService.PostMessage(message, int.Parse(userId));

            await Clients.Group(chat.chatKey)
                    .ReceiveSpecificMessage(int.Parse(userId), message);
        }
    }

}
