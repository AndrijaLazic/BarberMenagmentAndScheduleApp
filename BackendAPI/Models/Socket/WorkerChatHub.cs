using BackendAPI.Services.DataService;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BackendAPI.Models.Socket
{
    public class WorkerChatHub : Hub<IWorkerChatHub>
    {
        public readonly SharedDB _sharedDb;

        public WorkerChatHub(SharedDB sharedDb)
        {
            _sharedDb = sharedDb;
        }

        public override async Task OnConnectedAsync()
        {
        }

        /*public async Task JoinChat(UserConnection connection)
        {
            await Clients.All
                .SendAsync("Recieve message", "admin", $"{connection.Name} {connection.LastName} has joined");
        }*/

        /// <summary>
        ///     Creates a chat room with specified user
        /// </summary>
        /// <param name="user1Id">User that wants to connect</param>
        /// <param name="user2Id">User you want to connect with</param>
        /// <returns></returns>
        public async Task JoinChatWithUser(int user1Id, int user2Id)
        {
            WorkerChat chat = _sharedDb.AddWorkerChat(new WorkerChat(user1Id, user2Id), Context.ConnectionId);

            await Groups.AddToGroupAsync(Context.ConnectionId, chat.chatKey);


            Console.WriteLine(chat.user1Connection);
            Console.WriteLine(chat.user2Connection);

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
