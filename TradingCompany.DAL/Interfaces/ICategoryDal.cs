using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DTO;

namespace TradingCompany.DAL.Interfaces
{
    public interface ICategoryDal
    {
        Category Create(Category category);
        Category? GetById(int id);
        List<Category> GetAll();
        bool Update(Category category);
        bool Delete(int id);
    }
}
