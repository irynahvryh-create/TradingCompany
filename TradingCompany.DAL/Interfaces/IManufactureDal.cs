using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DTO;

namespace TradingCompany.DAL.Interfaces
{
    public interface IManufactureDal
    {
        Manufacture Create(Manufacture manufacture);       // Створення нового виробника
        Manufacture? GetById(int id);                       // Отримати виробника за ID
        List<Manufacture> GetAll();                         // Отримати всіх виробників
        bool Update(Manufacture manufacture);              // Оновити дані виробника
        bool Delete(int id);                               // Видалити виробника за ID
    }
}