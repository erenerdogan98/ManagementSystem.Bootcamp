using FluentValidation;
using ManagementSystem.DLL.Database;
using ManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.ValidationRules
{
    public class UserUpdateValidation : AbstractValidator<User>
    {
        Context dbcontext;
        public UserUpdateValidation(Context context)
        {
            dbcontext = context;
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Şifre boş geçilemez");
            RuleFor(x => x.NewPassword).NotEmpty().When(x => !string.IsNullOrEmpty(x.Password)).WithMessage("Yeni şifre boş geçilemez");
            RuleFor(x => x.NewPassword).MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır");
            RuleFor(x => x.NewPassword).Matches("[A-Za-z]").WithMessage("Şifre en az bir harf içermelidir");
            RuleFor(x => x.NewPassword).Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir");
        }
    }
}
