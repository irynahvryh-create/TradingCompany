using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.BL.Interfaces;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;

namespace Trading_company.BL.Concrete
{
    public class ProductManager : IProductManager
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public Product CreateProduct(Product product)
        {
            return _productDal.Create(product);
        }

        public bool DeleteProduct(int productId)
        {
            return _productDal.Delete(productId);
        }

        public List<Product> GetAllProducts()
        {
            return _productDal.GetAll();
        }

        public Product? GetProductById(int productId)
        {
            return _productDal.GetById(productId);
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductsByManufacturer(int manufacturerId)
        {
            throw new NotImplementedException();
        }

        public Product UpdateProduct(Product product)
        {

            bool success = _productDal.Update(product);
            if (!success)
                throw new Exception("Failed to update category");

            return product; // повертаємо оновлений об’єкт
        }
    }
}
