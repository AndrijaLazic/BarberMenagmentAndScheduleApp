using System;
using System.Collections.Generic;

namespace BackendAPI.Models.Database
{
    public partial class Worker
    {
        public Worker()
        {
            WorkerCommunications = new HashSet<WorkerCommunication>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int WorkerTypeId { get; set; }
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;

        public virtual WorkerType WorkerType { get; set; } = null!;
        public virtual ICollection<WorkerCommunication> WorkerCommunications { get; set; }
    }
}
