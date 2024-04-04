using BackendAPI.Models.Socket;
using System.Collections.Concurrent;

namespace BackendAPI.Services.DataService
{
    public class SharedDB
    {
        private ConcurrentDictionary<string, WorkerChat> _chatsBetweenWorkers=new ConcurrentDictionary<string, WorkerChat>();
        private ConcurrentDictionary<string, string> _onlineWorkers=new ConcurrentDictionary<string, string>();

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

        public WorkerChat AddWorkerChat(int user1Id, int user2Id, string userConnId,int chatID)
        {
            WorkerChat ?oldChat = GiveWorkerChat(user1Id, user2Id);
            if(oldChat != null)
            {
                if (oldChat.worker1.userConnectionId == null)
                {
                    oldChat.worker1.userConnectionId = userConnId;
                    return oldChat;
                }
                oldChat.worker2.userConnectionId = userConnId;
                return oldChat;
            }

            WorkerChat? newChat=new WorkerChat(user1Id, user2Id, chatID);
            newChat.worker1.userConnectionId = userConnId;
            _chatsBetweenWorkers.TryAdd(newChat.chatKey, newChat);
            return newChat;
        }

        public void setWorkerOnline(string workerId, string connection) {
            _onlineWorkers.TryAdd(workerId, connection);
        }

        public string? disconnectWorker(string connId) {
            KeyValuePair<string, string> pair = _onlineWorkers.FirstOrDefault(x => x.Value == connId);
            if (pair.Equals(default(KeyValuePair<string, string>)))
            {
                return null;
            }
            
            _onlineWorkers.TryRemove(pair.Key, out string? result);

            List<KeyValuePair<string, WorkerChat>> chatRooms = _chatsBetweenWorkers
                .Where(x => x.Value.worker1.userConnectionId == connId || x.Value.worker2.userConnectionId == connId).ToList();

            foreach(KeyValuePair<string, WorkerChat> room in chatRooms)
            {
                if(room.Value.worker1.userConnectionId == connId)
                {
                    room.Value.worker1.userConnectionId = "";
                }
                else
                {
                    room.Value.worker2.userConnectionId = "";
                }

                if(room.Value.worker1.userConnectionId == "" && room.Value.worker2.userConnectionId == "")
                {
                    _chatsBetweenWorkers.TryRemove(room.Key,out WorkerChat? roomChat);
                }
            }

            return pair.Key;
        }

        public string? isWorkerOnline(string workerId) {
            _onlineWorkers.TryGetValue(workerId, out string? connId);
            return connId;
        }

    }
}
