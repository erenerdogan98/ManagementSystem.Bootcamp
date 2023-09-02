using FluentValidation;
using ManagementSystem.BLL.Abstract;
using ManagementSystem.DLL.Database;
using ManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.ValidationRules
{
    public class UserLoginValidator : AbstractValidator<User>
    {
        private readonly Context dbContext; // buraya tanımlayıp , yapıcı metotta new ile örnekliyeceğiz
        private readonly IUserService _userService;
        public UserLoginValidator(Context context,IUserService userService)
        {
            dbContext = context;
            _userService = userService;
            #region Login işlemleri için kurallar
            RuleFor(x => x.Email).Must(BeRegisteredEmail).WithMessage("Bu e-posta adresiyle bir kayıt bulunamadı");
            RuleFor(x => x.Password).Must(IsRegisteredPassword).WithMessage("Geçersiz şifre");
            #endregion
        }
        #region Login işlemleri için 
        private bool BeRegisteredEmail(string email)
        {
            return _userService.GetAllUsers().Any(u => u.Email == email);
        }

        private bool IsRegisteredPassword(User user, string password)
        {
            return user.Password == password;
        }
        #endregion
    }
}
