using FluentValidation.Results;
using ManagementSystem.BLL;
using ManagementSystem.BLL.Abstract;
using ManagementSystem.BLL.ValidationRules;
using ManagementSystem.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserRegistrationValidator _userValidator;
        private readonly UserLoginValidator _userLoginValidator;
        private readonly IEventService _eventService;
        private readonly UserUpdateValidation _userUpdatevalidator;

        public UserController(IUserService userService, UserRegistrationValidator usserValidator, UserLoginValidator userLoginvalidator, IEventService eventService, UserUpdateValidation userUpdatevalidator)
        {
            _userService = userService;
            _userValidator = usserValidator;
            _userLoginValidator = userLoginvalidator;
            _eventService = eventService;
            _userUpdatevalidator = userUpdatevalidator;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            List<User> users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            User user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("register")] // kullanıcı kayıt için
        public IActionResult AddUser(User user)
        {
            ValidationResult results = _userValidator.Validate(user);
            if (results.IsValid && user.IsAdmin == false)
            {
                _userService.AddUser(user);

                return Ok($"{user.Name} {user.Surname} kaydınız başarılı bir şekilde oluşturuldu");
            }
            else if (results.IsValid && user.IsAdmin == true)
            {
                _userService.AddUser(user);
                return Ok($"{user.Name} {user.Surname} Admin olarak kaydınız oluşturuldu");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return BadRequest(results);
            }

        }
        [HttpPost("login")]
        public IActionResult Login(User user)
        {

            ValidationResult results = _userLoginValidator.Validate(user);
            if (results.IsValid)
            {
                _userService.Login(user.Email, user.Password);
                return Ok($"Tebrikler {user.Email} giriş işlemi başarılı");
            }

            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return BadRequest(results);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            User existingUser = _userService.GetUserById(id);
            ValidationResult result = _userUpdatevalidator.Validate(user);
            if (result.IsValid)
            {
                if (existingUser == null)
                {
                    return NotFound();
                }
                else
                {
                    user.ID = id;
                    _userService.UpdateUser(user);
                    return Ok("Kullanıcı başarıyla güncellendi");
                }
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

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            User user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            _userService.DeleteUser(user);
            return Ok();
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
            _eventService.UpdateEvent(existingEvent);
            return Ok();
        }

        [HttpPost("admin/rejectEvent/{eventId}")]
        public IActionResult RejectEvent(int eventId)
        {
            Event existingEvent = _eventService.GetEventByID(eventId);
            if (existingEvent == null)
            {
                return NotFound();
            }
            existingEvent.IsAdminApproval = false;
            _eventService.UpdateEvent(existingEvent);
            return Ok();
        }
    }
}




