using BackendAPI.Models.Database;
using BackendAPI.Services.DataService;
using BackendAPI.Services.WorkerService;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BackendAPI.Models.Socket
{
    public class WorkerSocketHub : Hub<IWorkerChatHub>
    {
        public readonly SharedDB _sharedDb;

        public WorkerSocketHub(SharedDB sharedDb)
        {
            _sharedDb = sharedDb;
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
        public async Task JoinServer(string JWT)
        {
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
            WorkerChat chat = _sharedDb.AddWorkerChat(user1Id, user2Id, Context.ConnectionId);

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
