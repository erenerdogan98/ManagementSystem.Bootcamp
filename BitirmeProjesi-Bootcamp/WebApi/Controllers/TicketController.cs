using FluentValidation.Results;
using ManagementSystem.BLL.Abstract;
using ManagementSystem.BLL.ValidationRules;
using ManagementSystem.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketForCompaniesService _ticketForCompaniesService;
        private readonly ICompanyTicketService _companyTicketService;
        private readonly IEventService _eventService;
        private readonly TicketValidator _ticketvalidator;
        public TicketController(ITicketForCompaniesService ticketForCompaniesService, ICompanyTicketService companyTicketService, IEventService eventService, TicketValidator validationRules)
        {
            _ticketForCompaniesService = ticketForCompaniesService;
            _companyTicketService = companyTicketService;
            _eventService = eventService;
            _ticketvalidator = validationRules;
        }
        [HttpGet("events")]
        public IActionResult GetAllEvents()
        {
            List<Event> events = _eventService.GetAllEvents();
            return Ok(events);
        }

        [HttpGet("event/{eventid}")]
        public IActionResult GetEventbyId(int id)
        {
            Event e = _eventService.GetEventByID(id);
            if (e == null)
            {
                return NotFound();
            }
            return Ok(e);
        }

        [HttpPost("events/{eventId}/tickets")]
        public IActionResult AddTicket(int eventId, Ticket ticket)
        {
            ValidationResult result = _ticketvalidator.Validate(ticket);

            if (result.IsValid)
            {
                Event e = _eventService.GetEventByID(eventId);
                if (e == null)
                {
                    return NotFound();
                }
                _ticketForCompaniesService.AddTicket(ticket);
                return Ok("İşleminiz başarıyla gerçekleştirilmiştir");
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
    }
}
