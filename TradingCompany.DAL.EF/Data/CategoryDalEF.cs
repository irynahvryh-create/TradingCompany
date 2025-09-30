using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.DAL.EF.Data
{
    public class CategoryDalEF : ICategoryDal
    {
        private readonly string _connStr;
        public CategoryDalEF(string connStr)
        {
            _connStr = connStr;
        }

        public Category Create(Category category)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAll()
        {
            using (var context = new TradingCompanyContext())
            {
                return context.Categories.Select(
                    m=> new  TradingCompany.DTO.Category
                    {
                        CategoryID = m.CategoryId,
                        Name = m.Name,
                        Status = m.Status
                    }).ToList();
            }
        }

        public Category? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
