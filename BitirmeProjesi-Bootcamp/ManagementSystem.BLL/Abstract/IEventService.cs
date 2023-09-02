using ManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.Abstract
{
    public interface IEventService
    {
        void AddEvent(Event e);
        void UpdateEvent(Event e);
        void DeleteEvent(Event e);
        List<Event> GetAllEvents();
        Event GetEventByID(int id);
        

    }
}
