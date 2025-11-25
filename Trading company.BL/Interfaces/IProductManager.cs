using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DTO;

namespace TradingCompany.BL.Interfaces
{
    
        public interface IProductManager
        {
            List<Product> GetAllProducts();
            Product CreateProduct(Product product);
            Product? GetProductById(int productId);
            Product UpdateProduct(Product product);
            bool DeleteProduct(int productId);

            List<Product> GetProductsByCategory(int categoryId);
            List<Product> GetProductsByManufacturer(int manufacturerId);
        }
    }


