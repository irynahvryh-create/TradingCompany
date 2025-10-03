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
        Manufacture Create(Manufacture manufacture);       
        Manufacture? GetById(int id);                       
        List<Manufacture> GetAll();                        
        bool Update(Manufacture manufacture);              
        bool Delete(int id);                               
    }
}