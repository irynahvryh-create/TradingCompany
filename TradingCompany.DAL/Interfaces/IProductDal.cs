using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DTO;

namespace TradingCompany.DAL.Interfaces
{
    public interface IProductDal
    {
        Product Create(Product product);       
        Product? GetById(int id);             
        List<Product> GetAll();                
        bool Update(Product product);          
        bool Delete(int id);                  
    }
}
