namespace BackendAPI.Models.Socket
{
    public interface IWorkerChatHub
    {
        public Task ReceiveMessage(string message);
        public Task JoinedMessage(string message);
        public Task JoinedServerMessage(string message);
        public Task DisconnectedFromAppMessage(string message);

        public Task ReceiveSpecificMessage(int receiverId,string message);

        public Task ValidationError(string message);
    }
}
