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
    public class ProductLogManager : IProductLogManager

    {

        private readonly IProductLogDal _prodactlogDal;
        public ProductLogManager(IProductLogDal productlogDal)
        {
            _prodactlogDal = productlogDal;
        }
        public ProductLog CreateProductLog(ProductLog productlog)
        {
           return productlog;
        }

        public bool DeleteProductLog(int productlogId)
        {
           return _prodactlogDal.Delete(productlogId);
        }

        public List<ProductLog> GetAllProductLog()
        {
            return _prodactlogDal.GetAll();
        }

        public ProductLog? GetProductLogById(int productlogId)
        {
         return _prodactlogDal.GetById(productlogId);
        }

        public ProductLog UpdateProductLog(ProductLog productlog)
        {
           bool success = _prodactlogDal.Delete(productlog.LogID);
            if (success)
            {
                return _prodactlogDal.Create(productlog);
            }
            return productlog;
        }
    }
}
