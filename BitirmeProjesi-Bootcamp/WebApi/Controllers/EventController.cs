using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using ManagementSystem.BLL.Abstract;
using ManagementSystem.BLL.ValidationRules;
using ManagementSystem.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly EventCreateValidator _eventvalidator;
        private readonly ICityService _cityService;
        private readonly ICategoryService _categoryService; // Category seçimi için , category a ulaşmamız için 
        public EventController(IEventService eventService, EventCreateValidator eventvalidator, ICategoryService categoryService, ICityService cityService)
        {
            _eventService = eventService;
            _eventvalidator = eventvalidator;
            _categoryService = categoryService;
            _cityService = cityService;
        }
        [HttpGet("events")]
        public IActionResult GetAllEvents()
        {
            List<Event> events = _eventService.GetAllEvents();
            return Ok(events);
        }
        [HttpGet("event/{id}")]
        public IActionResult GetEventbyId(int id)
        {
            Event e = _eventService.GetEventByID(id);
            if (e == null)
            {
                return NotFound();
            }
            return Ok(e);
        }
        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            List<Category> categories = _categoryService.GetAllCategories();
            return Ok(categories);
        }
        [HttpGet("Cities")]
        public IActionResult GetCities()
        {
            List<City> cities = _cityService.GetAll();
            return Ok(cities);
        }
        [HttpPost]
        public IActionResult CreateEvent(Event e, int categoryId)
        {
            ValidationResult result = _eventvalidator.Validate(e);
            if (result.IsValid)
            {
                e.IsAdminApproval = false;
                e.IsActive = false;

                _eventService.AddEvent(e);

                return Ok($"Etkinliğiniz {e.EventName} başarılı bir şekilde oluşturuldu.Admin onayını bekleyiniz");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return BadRequest(result);
            }
            
        }

        [HttpPost("admin/approveEvent/{eventId}")]
        public IActionResult ApproveEvent(int eventId)
        {
            Event existingEvent = _eventService.GetEventByID(eventId);
            if (existingEvent == null)
            {
                return NotFound();
            }
            existingEvent.IsAdminApproval = true;
            existingEvent.IsActive = true;
            _eventService.UpdateEvent(existingEvent);

           

            return Ok("Etkinlik admin tarafından onaylandı.");
        }

        [HttpPost("admin/rejectEvent/{eventId}")]
        public IActionResult RejectEvent(int eventId)
        {
            Event existingEvent = _eventService.GetEventByID(eventId);
            if (existingEvent == null)
            {
                return NotFound();
            }
            else if (!existingEvent.IsAdminApproval)
            {
                _eventService.DeleteEvent(existingEvent);
                return Ok("Event admin tarafından reddedildi ve kaldırıldı.");
            }         
             else
            {
                return BadRequest("Etkinlik zaten onaylandı ve reddedilemez.");
            }      
        }
        [HttpPost("{eventId}/update")]
        public IActionResult UpdateEvent(int eventId, Event updatedEvent)
        {
            Event existingEvent = _eventService.GetEventByID(eventId);
            if (existingEvent == null)
            {
                return NotFound();
            }

            DateTime fiveDaysBeforeEvent = existingEvent.EventDate.AddDays(-5);
            if (DateTime.Now >= fiveDaysBeforeEvent)
            {
                return BadRequest("Etkinlik başlangıcına 5 günden az kaldığı için güncelleme yapılamaz.");
            }

            // Kontenjan ve etkinlik adresi güncelleme işlemleri
            if (existingEvent.IsAdminApproval && !existingEvent.IsActive)
            {
                // Etkinlik henüz başlamamış, güncelleme yapılabilir
                existingEvent.EventCapacity = updatedEvent.EventCapacity;
                existingEvent.Address = updatedEvent.Address;
                _eventService.UpdateEvent(existingEvent);

                return Ok($"Etkinlik bilgileri {existingEvent.EventCapacity} ve/veya {existingEvent.Address} güncellendi.");
            }
            else if (existingEvent.IsActive)
            {
                return BadRequest("Etkinlik başladığı için güncelleme yapılamaz.");
            }
            else
            {
                return BadRequest("Etkinlik admin onayı bekliyor, güncelleme yapılamaz.");
            }
        }

        [HttpPost("{eventId}/cancel")]
        public IActionResult CancelEvent(int eventId)
        {
            Event existingEvent = _eventService.GetEventByID(eventId);
            if (existingEvent == null)
            {
                return NotFound();
            }
            #region 5 gün kala şartı için

            DateTime fiveDaysBeforeEvent = existingEvent.EventDate.AddDays(-5);

            if(DateTime.Now >= fiveDaysBeforeEvent) // else if yazıcaktım ama bende öğrenmiş oldum , üstüne parametre tanımlayınca else if ile başlanmıyor :)
            {
                return BadRequest("Etkinlik başlangıcına 5 günden az kaldığı için iptal edilemez.");
            }
            #endregion
            // Etkinlik iptal işlemi
            else if(existingEvent.IsAdminApproval && !existingEvent.IsActive)
            {
               
                _eventService.DeleteEvent(existingEvent);

                return Ok("Etkinlik iptal edildi.");
            }
            else if(existingEvent.IsActive)
            {
                return BadRequest("Etkinlik başladığı için iptal edilemez.");
            }
            else
            {
                return BadRequest("Etkinlik admin onayı bekliyor, iptal edilemez.");
            }
        }
    }
}
