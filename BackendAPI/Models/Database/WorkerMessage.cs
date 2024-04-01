using System;
using System.Collections.Generic;

namespace BackendAPI.Models.Database
{
    public partial class WorkerMessage
    {
        public int CommunicationId { get; set; }
        public string Message { get; set; } = null!;
        public int SenderID { get; set; }

        public virtual WorkerCommunication Communication { get; set; } = null!;
    }
}
