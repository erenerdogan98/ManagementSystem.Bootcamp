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
    public class TicketValidator : AbstractValidator<Ticket>
    {
        private readonly Context dbContext;
        public TicketValidator(Context context)
        {
            dbContext = context;

            RuleFor(x => x.Company).NotEmpty().WithMessage("Firma adı boş geçilemez");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Geçerli bir fiyat girin");
            RuleFor(x => x.PurchaseDate).NotEmpty().WithMessage("Satın alma tarihi boş geçilemez");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Geçerli bir miktar girin");
        }
    }
}
