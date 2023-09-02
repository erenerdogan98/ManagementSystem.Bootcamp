using FluentValidation.Results;
using ManagementSystem.BLL.Abstract;
using ManagementSystem.BLL.ValidationRules;
using ManagementSystem.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly CityAddValidator _validationRules;
        public CityController(ICityService cityService, CityAddValidator validationRules)
        {
            _cityService = cityService;
            _validationRules = validationRules;
        }
        [HttpGet]
        public IActionResult GetCities()
        {
            List<City> cities = _cityService.GetAll();
            return Ok(cities);
        }
        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            City city = _cityService.GeyCityByID(id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
        [HttpPost]
        public IActionResult AddCity(City city)
        {
            ValidationResult validationResult = _validationRules.Validate(city);
            if (validationResult.IsValid)
            {
                _cityService.AddCity(city);
                return Ok($"{city.CityName} sisteme başarılı bir şekilde eklenmiştir");
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
    }
}
