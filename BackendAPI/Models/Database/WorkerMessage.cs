using System;
using System.Collections.Generic;

namespace BackendAPI.Models
{
    public partial class WorkerMessage
    {
        public int Id { get; set; }
        public int CommunicationId { get; set; }
        public string Message { get; set; } = null!;
        public int SenderId { get; set; }

        public virtual WorkerCommunication Communication { get; set; } = null!;
    }
}
