using ManagementSystem.BLL.Abstract;
using ManagementSystem.DLL.EntityFramework;
using ManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        EFCategoryRepository categoryRepository;
        public CategoryService()
        {
            categoryRepository = new EFCategoryRepository();
        }
        public void AddCategory(Category category)
        {
            categoryRepository.Insert(category);
        }

        public void DeleteCategory(Category category)
        {
            categoryRepository.Delete(category);
        }

        public List<Category> GetAllCategories()
        {
            return categoryRepository.GetAll();
        }

        public Category GetCategory(int id)
        {
            return categoryRepository.GetByID(id);
        }

        public void UpdateCategory(Category category)
        {
            categoryRepository.Update(category);
        }
    }
}
