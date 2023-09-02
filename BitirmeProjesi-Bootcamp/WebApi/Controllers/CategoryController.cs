using Microsoft.AspNetCore.Http;
using FluentValidation.Results;
using ManagementSystem.BLL.Abstract;
using ManagementSystem.BLL.ValidationRules;
using ManagementSystem.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly CategoryAddValidator validations;
        public CategoryController(ICategoryService categoryService, CategoryAddValidator Categoryvalidations)
        {
            _categoryService = categoryService;
             validations = Categoryvalidations;

        }
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            List<Category> categories = _categoryService.GetAllCategories();
            return new JsonResult(categories); // bunu tab önerdiği için deniyorum 
        }
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            Category category = _categoryService.GetCategory(id);
            if(category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            ValidationResult result = validations.Validate(category);
            if(result.IsValid)
            {
                _categoryService.AddCategory(category);
                return Ok($"{category} başarılı bir şekilde eklendi");
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
