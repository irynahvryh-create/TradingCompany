using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.DAL.EF.Data
{
    public class ProductLogDalEF : IProductLogDal
    {
        public ProductLog Create(ProductLog log)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductLog> GetAll()
        {
            throw new NotImplementedException();
        }

        public ProductLog? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductLog> GetByProductId(int productId)
        {
            throw new NotImplementedException();
        }

        public bool Update(ProductLog log)
        {
            throw new NotImplementedException();
        }
    }
}
