using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.DAL.EF.Data
{
    public class ManufacturerDalEF : IManufacturerDal
    {
        private readonly string _connStr;
        public ManufacturerDalEF(string connStr)
        {
            _connStr = connStr;
        }
        public Manufacturer Create(Manufacturer manufacturer)
        {
            throw new NotImplementedException();
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public List<Manufacturer> GetAll()
        {
            throw new NotImplementedException();
        }
        public Manufacturer? GetById(int id)
        {
            throw new NotImplementedException();
        }
        public bool Update(Manufacturer manufacturer)
        {
            throw new NotImplementedException();
        }
    }
}
