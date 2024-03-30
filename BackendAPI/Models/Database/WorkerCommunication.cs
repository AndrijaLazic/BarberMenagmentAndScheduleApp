using System;
using System.Collections.Generic;

namespace BackendAPI.Models.Database
{
    public partial class WorkerCommunication
    {
        public int Id { get; set; }
        public int User1 { get; set; }
        public int User2 { get; set; }
        public int UnreadMessages { get; set; }

        public virtual Worker User1Navigation { get; set; } = null!;
    }
}
