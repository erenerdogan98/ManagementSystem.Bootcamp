using FluentValidation;
using ManagementSystem.BLL.Abstract;
using ManagementSystem.DLL.Database;
using ManagementSystem.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.ValidationRules
{
    public class EventCreateValidator : AbstractValidator<Event>
    {
        private readonly Context dbContext;
        private readonly ICategoryService _categoryService;
        private readonly ICityService _cityService;
        public EventCreateValidator(Context context,ICategoryService categoryService,ICityService cityService)
        {
               dbContext = context;
            _categoryService = categoryService;
            _cityService = cityService;
            //    RuleFor(x => x.IsAdminApproval).Must(BeApprovedByAdmin).WithMessage("Admin tarafından onaylanmadı");
            RuleFor(x => x.Category).Must(RegisteredCategoryinSystem).WithMessage("Lütfen listedeki Category lerden birini seçiniz");
            RuleFor(x => x.City).Must(RegisteredCityinStstem).WithMessage("Lütfen listedeki şehirlerden birini seçiniz");
        }

        private bool RegisteredCategoryinSystem(string category)
        {
            bool registered = _categoryService.GetAllCategories().Any(x => x.CategoryName == category);
            return registered;
        }
        private bool RegisteredCityinStstem(string city)
        {
            bool registered = _cityService.GetAll().Any(x => x.CityName == city);
            return registered;
        }

    }
}
