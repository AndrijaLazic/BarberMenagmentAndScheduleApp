namespace BackendAPI.Models.DTO
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public int CommunicationId { get; set; }
        public string ?Message { get; set; }
        public int SenderId { get; set; }
    }
}
