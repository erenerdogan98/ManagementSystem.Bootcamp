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
    public class EventService : IEventService
    {
        EFEventRepository efeventRepository;
        public EventService()
        {
            efeventRepository = new EFEventRepository();
        }

        public void AddEvent(Event e)
        {
            efeventRepository.Insert(e);
        }

        public void DeleteEvent(Event e)
        {
            efeventRepository.Delete(e);
        }

        public List<Event> GetAllEvents()
        {
            return efeventRepository.GetAll();
        }

        public Event GetEventByID(int id)
        {
            return efeventRepository.GetByID(id);
        }

        public void UpdateEvent(Event e)
        {
            efeventRepository.Update(e);
        }
    }
}
