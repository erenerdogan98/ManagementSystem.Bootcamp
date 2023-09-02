using ManagementSystem.BLL;
using ManagementSystem.BLL.Abstract;
using ManagementSystem.BLL.ValidationRules;
using ManagementSystem.DLL.Database;
using ManagementSystem.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly Context dbContext;
        private readonly UserValidator userValidator;
        public UserController(IUserService userService,Context dbContext)
        {
            this.userService = userService;
            this.dbContext = dbContext;
            this.userValidator = new UserValidator(dbContext);
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            // UserValidator sınıfını kullanarak user nesnesini doğrulayın
            var validationResult = userValidator.Validate(user);
            if (!validationResult.IsValid)
            {
                // user geçerli değilse doğrulama hatalarını döndürün
                return BadRequest(validationResult.Errors);
            }

            // UserService'in AddUser metodunu çağırarak kullanıcıyı ekleyin
            userService.AddUser(user);

            // Başarılı bir yanıt döndürün
            return Ok("Kullanıcı başarıyla kaydedildi");
        }
    }
}
