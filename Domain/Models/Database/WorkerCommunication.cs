using System;
using System.Collections.Generic;

namespace Domain.Models.Database
{
    public partial class WorkerCommunication
    {
        public WorkerCommunication()
        {
            WorkerMessages = new HashSet<WorkerMessage>();
        }

        public int Id { get; set; }
        public int User1 { get; set; }
        public int User2 { get; set; }
        public int UnreadMessages { get; set; }

        public virtual Worker User1Navigation { get; set; } = null!;
        public virtual ICollection<WorkerMessage> WorkerMessages { get; set; }
    }
}
