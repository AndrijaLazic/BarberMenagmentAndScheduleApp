using System;
using System.Collections.Generic;

namespace BackendAPI.Models
{
    public partial class WorkerType
    {
        public WorkerType()
        {
            Workers = new HashSet<Worker>();
        }

        public int Id { get; set; }
        public string WorkerType1 { get; set; } = null!;

        public virtual ICollection<Worker> Workers { get; set; }
    }
}
