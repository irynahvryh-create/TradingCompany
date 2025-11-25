using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DTO;

namespace Trading_company.BL.Interfaces
{
    public interface IProductLogManager
    {
        List<ProductLog> GetAllProductLog();
        ProductLog CreateProductLog(ProductLog productlog);
        ProductLog? GetProductLogById(int productlogId);
        ProductLog UpdateProductLog(ProductLog productlog);
        bool DeleteProductLog(int productlogId);


    }
}
