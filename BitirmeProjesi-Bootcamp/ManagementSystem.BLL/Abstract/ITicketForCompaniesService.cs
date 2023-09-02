using ManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.Abstract
{
    public interface ITicketForCompaniesService
    {
        void AddTicket(Ticket t);
        void UpdateTicket(Ticket t);
        void DeleteTicket(Ticket t);
        Ticket GetTicketByID(int id);
        List<Ticket> GetAll();
    }
}
