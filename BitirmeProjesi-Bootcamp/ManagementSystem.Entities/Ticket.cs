using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Entities
{
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; } // primary key
        public int _userID { get; set; }
        public int _eventID { get; set; }
        public int _CompanyID { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Quantity { get; set; }

        // virtual olarak diğer gerekli entity sınıfları ile bağlantı kuruyoruz
        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
        public virtual Company Company { get; set; }
    }
}
