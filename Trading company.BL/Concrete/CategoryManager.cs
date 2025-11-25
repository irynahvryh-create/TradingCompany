using TradingCompany.BL.Interfaces;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;
using System.Collections.Generic;

namespace TradingCompany.BL.Concrete
{
    public class CategoryManager : ICategoryManager
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public List<Category> GetAllCategories()
        {
            return _categoryDal.GetAll();
        }

        public Category CreateCategory(Category category)
        {

            return _categoryDal.Create(category);
        }

        public Category? GetCategoryById(int categoryId)
        {
            return _categoryDal.GetById(categoryId);
        }

        public Category UpdateCategory(Category category)
        {
            bool success = _categoryDal.Update(category);
            if (!success)
                throw new Exception("Failed to update category");

            return category; // повертаємо оновлений об’єкт
        }


        public bool DeleteCategory(int categoryId)
        {
            return _categoryDal.Delete(categoryId);
        }
    }
}
