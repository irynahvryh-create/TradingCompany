using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading_company.BL.Interfaces;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;

namespace Trading_company.BL.Concrete
{
    public class ManufactureManager : IManufactureManager
    {
        private readonly IManufactureDal _manufactureDal;
        public ManufactureManager(IManufactureDal manufactureDal)
        {
            _manufactureDal = manufactureDal;
        }
        public Manufacture CreateManufacture(Manufacture manufacture)
        {
            return _manufactureDal.Create(manufacture);
        }

        public bool DeleteManufacture(int manufactureId)
        {
            return _manufactureDal.Delete(manufactureId);
        }

        public List<Manufacture> GetAllManufactures()
        {
            return _manufactureDal.GetAll();
        }

        public Manufacture? GetManufactureById(int manufactureId)
        {
           return _manufactureDal.GetById(manufactureId);
        }

        public Manufacture UpdateManufacture(Manufacture manufacture)
        {
            bool success = _manufactureDal.Update(manufacture);
            if (!success)
                throw new Exception("Failed to update manufacture");
            return manufacture; 
        }
    }
}
