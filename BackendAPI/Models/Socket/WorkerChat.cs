namespace BackendAPI.Models.Socket
{
    public class WorkerChat
    {
        public readonly string chatKey;
        public readonly int user1Id;
        public readonly int user2Id;
        public string user1Connection;
        public string user2Connection;

        public WorkerChat(int user1Id, int user2Id)
        {
            this.user1Id = user1Id;
            this.user2Id = user2Id;
            this.chatKey = $"{user1Id}"+"/"+$"{user2Id}";
        }
    }
}
