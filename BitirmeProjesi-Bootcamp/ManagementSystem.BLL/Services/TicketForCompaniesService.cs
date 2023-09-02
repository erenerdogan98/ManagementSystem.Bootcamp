using ManagementSystem.BLL.Abstract;
using ManagementSystem.DLL.EntityFramework;
using ManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.Services
{
    public class TicketForCompaniesService : ITicketForCompaniesService
    {
        EFTicketRepository _efticket;
        public TicketForCompaniesService()
        {
            _efticket = new EFTicketRepository();
        }
        public void AddTicket(Ticket t)
        {
            _efticket.Insert(t);
        }

        public void DeleteTicket(Ticket t)
        {
            _efticket.Delete(t);
        }

        public List<Ticket> GetAll()
        {
            return _efticket.GetAll();
        }

        public Ticket GetTicketByID(int id)
        {
            return _efticket.GetByID(id);
        }

        public void UpdateTicket(Ticket t)
        {
            _efticket.Update(t);
        }
    }
}
