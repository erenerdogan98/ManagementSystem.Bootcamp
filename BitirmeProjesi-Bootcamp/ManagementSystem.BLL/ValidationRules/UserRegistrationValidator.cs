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
    public class UserRegistrationValidator : AbstractValidator<User>
    {
        private readonly Context dbContext; // buraya tanımlayıp , yapıcı metotta new ile örnekliyeceğiz
        //private readonly IUserService _userService;
        public UserRegistrationValidator(Context context)
        {
            dbContext = context;
            //_userService = userService;
            RuleFor(x => x.Email).Must(BeUniqueEmail).WithMessage("Bu e-posta adresiyle zaten bir üyelik mevcut");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş geçilemez");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır");
            RuleFor(x => x.Password).Matches("[A-Za-z]").WithMessage("Şifre en az bir harf içermelidir");
            RuleFor(x => x.Password).Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir");

           

            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim boş geçilemez");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyisim boş geçilemez");

        }

        private bool BeUniqueEmail(string email)
        {
            bool isEmailUnique = !dbContext.Users.Any(u => u.Email == email);
            return isEmailUnique;
        }      
    }
}
//public UserValidator()
//{
//    RuleFor(x=>x.Password).NotEmpty().WithMessage("Şifre boş geçilemez");
//    RuleFor(x=>x.Password).MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır");
//    RuleFor(x => x.Password).Matches("[A-Za-z]").WithMessage("Şifre en az bir harf içermelidir");
//    RuleFor(x => x.Password).Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir");    
//} 
