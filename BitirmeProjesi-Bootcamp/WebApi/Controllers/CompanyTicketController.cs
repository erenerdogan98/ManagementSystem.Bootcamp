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
    public class CompanyTicketController : ControllerBase
    {
        private readonly ICompanyTicketService _companyService;
        private readonly CompanyTicketValidator _validator;

        public CompanyTicketController(ICompanyTicketService companyTicketService, CompanyTicketValidator validationRules)
        {
            _companyService = companyTicketService;
            _validator = validationRules;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var companies = _companyService.GetAllTicketCompanies(); // daha önceki controller larda List<> olarak yaptım , burada var ile denemek istedim.
            return Ok(companies);
        }
        [HttpGet("id")]
        public IActionResult GetCompanyTicket(int id)
        {
            var company = _companyService.GetTicketCompany(id);
            if (company != null)
            {
                return Ok(company);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult AddCompanyTicket(Company company)
        {
            ValidationResult validationResult = _validator.Validate(company);
            if (validationResult.IsValid)
            {
                _companyService.AddTicketCompany(company);
                return Ok($"{company.CompanyName} sistemimize başarılı şekilde eklenmiştir");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return BadRequest(validationResult);
            }
        }
        [HttpPut("id")]
        public IActionResult Update(int id, Company company)
        {
            if (company == null)
            {
                return NotFound();
            }
            else if (company.CompanyID != id)
            {
                return BadRequest("Girmiş olduğunuz id değeri eşleşmiyor!");
            }
            company.CompanyID = id;
            _companyService.UpdateTicketCompany(company);
            return Ok("Güncelleme işleminiz başarılı bir şekilde gerçekleştirilmiştir");
        }
        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            var company = _companyService.GetTicketCompany(id);
            if (company != null)
            {
                _companyService.DeleteTicketCompany(company);
                return Ok("Silme işleminiz gerçekleştirilmiştir");
            }
            else { return NotFound(); }
        }
    }
}
