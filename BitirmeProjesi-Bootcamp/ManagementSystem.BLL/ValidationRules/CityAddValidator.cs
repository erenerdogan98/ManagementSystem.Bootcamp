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
    public class CityAddValidator : AbstractValidator<City>
    {
        private readonly Context dbcontext;
        public CityAddValidator(Context context)
        {
            dbcontext = context;
            RuleFor(x => x.CityName).Must(IsCityNameUnique).WithMessage("Sistemde kayıtlı olan şehir i bir daha ekleyemezsiniz");
        }
        private bool IsCityNameUnique(string city)
        {
            bool isCityUnique = !dbcontext.cities.Any(x=> x.CityName == city);
            return isCityUnique;
        }
    }
}
