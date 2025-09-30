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
        Product Create(Product product);       // Створення нового продукту
        Product? GetById(int id);              // Отримати продукт за ID
        List<Product> GetAll();                // Отримати всі продукти
        bool Update(Product product);          // Оновити дані продукту
        bool Delete(int id);                   // Видалити продукт за ID
    }
}
