namespace BackendAPI.Models.DTO
{
    public class WorketDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int WorkerTypeId { get; set; }
    }
}
