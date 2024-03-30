using System;
using System.Collections.Generic;

namespace BackendAPI.Models.Database
{
    public partial class WorkerType
    {
        public WorkerType()
        {
            Workers = new HashSet<Worker>();
        }

        public string WorkerType1 { get; set; } = null!;
        public int Id { get; set; }

        public virtual ICollection<Worker> Workers { get; set; }
    }
}
