using TradingCompany.DTO;
using System.Collections.Generic;

namespace TradingCompany.BL.Interfaces
{
    public interface ICategoryManager
    {
        List<Category> GetAllCategories();
        Category CreateCategory(Category category);
        Category? GetCategoryById(int categoryId);
        Category UpdateCategory(Category category);
        bool DeleteCategory(int categoryId);
    }
}
