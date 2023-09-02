using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Entities
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventApplicationDeadline { get; set; }
        public int EventCapacity { get; set; }
        public bool IsTicketed { get; set; }
        public bool IsAdminApproval { get; set; }
        public bool IsActive { get; set; }
        // public int CategoryName { get; set; }
    }
}
