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
    public class CategoryAddValidator : AbstractValidator<Category>
    {
        private readonly Context dbContext;
        public CategoryAddValidator(Context context)
        {
            dbContext = context;

            RuleFor(x => x.CategoryName).Must(BeUniqueCategoryName).WithMessage("Aynı kategori adını tekrar ekleyemezsiniz!");
        }
        private bool BeUniqueCategoryName(string categoryName)
        {
            bool isUniqueCategory = !dbContext.Categories.Any(x => x.CategoryName == categoryName);
            return isUniqueCategory;
    }
    }
  
}
