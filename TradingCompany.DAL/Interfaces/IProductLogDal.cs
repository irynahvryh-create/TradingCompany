using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DTO;

namespace TradingCompany.DAL.Interfaces
{
    public interface IProductLogDal
    {
        ProductLog Create(ProductLog log);
        ProductLog? GetById(int id);
        List<ProductLog> GetAll();
        List<ProductLog> GetByProductId(int productId); 
        bool Update(ProductLog log);
        bool Delete(int id);
    }
}
