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
    public class CompanyTicketValidator : AbstractValidator<Company>
    {
        private readonly Context dbContext;
        public CompanyTicketValidator(Context context)
        {
            dbContext = context;
            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Firma adı boş geçilemez");
            RuleFor(x => x.CompanyWebSite).NotEmpty().WithMessage("Web sitesi boş geçilemez");
            RuleFor(x => x.EmailAdress).NotEmpty().WithMessage("E-posta boş geçilemez").EmailAddress().WithMessage("Geçerli bir e-posta adresi girin");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş geçilemez");
        }
    }
}
