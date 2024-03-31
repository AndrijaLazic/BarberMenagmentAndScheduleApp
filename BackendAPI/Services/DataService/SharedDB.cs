using BackendAPI.Models.Socket;
using System.Collections.Concurrent;

namespace BackendAPI.Services.DataService
{
    public class SharedDB
    {
        private ConcurrentDictionary<string, WorkerChat> _chatsBetweenWorkers=new ConcurrentDictionary<string, WorkerChat>();

        public WorkerChat? GiveWorkerChat(int workerOneId, int workerTwoId)
        {
            if (_chatsBetweenWorkers.TryGetValue($"{workerOneId}" + "/" + $"{workerTwoId}", out WorkerChat chat1))
            {
                return chat1;
            }
            if (_chatsBetweenWorkers.TryGetValue($"{workerTwoId}" + "/" + $"{workerOneId}", out WorkerChat chat2))
            {
                return chat2;
            }
            return null;
        }

        public WorkerChat AddWorkerChat(WorkerChat chat, string userConnId)
        {
            WorkerChat ?newChat = GiveWorkerChat(chat.user1Id, chat.user2Id);
            if(newChat!=null)
            {
                if (newChat.user1Connection == null)
                {
                    newChat.user1Connection = userConnId;
                    return newChat;
                }
                newChat.user2Connection = userConnId;
                return newChat;
            }
            chat.user1Connection = userConnId;
            _chatsBetweenWorkers.TryAdd(chat.chatKey, chat);
            return chat;
        }
    }
}
