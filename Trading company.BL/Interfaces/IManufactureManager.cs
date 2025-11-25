using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DTO;

namespace Trading_company.BL.Interfaces
{
   public interface IManufactureManager
    {

        List<Manufacture> GetAllManufactures();
        Manufacture CreateManufacture(Manufacture manufacture);
        Manufacture? GetManufactureById(int manufactureId);
        Manufacture UpdateManufacture(Manufacture manufacture);
        bool DeleteManufacture(int manufactureId);
    }
}
