using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DTO;

namespace TradingCompany.DAL.Interfaces
{
    public interface IManufacturerDal
    {
        Manufacturer Create(Manufacturer manufacturer);       // Створення нового виробника
        Manufacturer? GetById(int id);                       // Отримати виробника за ID
        List<Manufacturer> GetAll();                         // Отримати всіх виробників
        bool Update(Manufacturer manufacturer);              // Оновити дані виробника
        bool Delete(int id);                                 // Видалити виробника за ID
    }
}
