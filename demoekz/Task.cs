using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoekz
{
    public class Task
    {
        public Guid TaskId { get; set; }
        public int TaskNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public string ProjectName { get; set; }
        public string TaskDescription { get; set; }
        public string Priority { get; set; }
        public string Assignee { get; set; }
        public string Status { get; set; }
        public DateTime CompletionDate { get; set; }
        public string CreatedBy { get; set; }
    }

}
