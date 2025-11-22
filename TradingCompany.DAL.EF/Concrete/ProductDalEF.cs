using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DAL.EF.Data;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.DAL.EF.Concrete
{
    public partial class ProductDalEF : IProductDal

    {
        private readonly string _connStr;
        private readonly IMapper _mapper;
        public ProductDalEF(string connStr, IMapper mapper)
        {
            _connStr = connStr;
            _mapper = mapper;
        }

        public Product Create(Product product)
        {
            using (var context = new TradingCompanyContext())
            {
                var entity = _mapper.Map<Models.Product>(product); // DTO → Entity
                context.Products.Add(entity);
                context.SaveChanges();

                return _mapper.Map<Product>(entity); // Повертаємо DTO
            }
        }

        public bool Delete(int id)
        {
            using (var context = new TradingCompanyContext())
            {
                var entity = context.Products.Find(id);
                if (entity == null)
                    return false;
                context.Products.Remove(entity);
                context.SaveChanges();
                return true;
            }
        }

        public List<Product> GetAll()
        {
            using (var context = new TradingCompanyContext())
            {
                var entities = context.Products.ToList();
                return _mapper.Map<List<Product>>(entities); // Entity → DTO
             }


        }

        public Product? GetById(int id)
        {
            using (var context = new TradingCompanyContext())
            {
                var entity = context.Products.Find(id);
                return entity == null ? null : _mapper.Map<Product>(entity); // Entity → DTO
            }
        }

        public bool Update(Product product)
        {
           using (var context = new TradingCompanyContext())
            {
                var entity = context.Products.Find(product.ProductID);
                if (entity == null)
                    return false;
                // Оновлюємо поля
                entity.Name = product.Name;
                entity.CategoryId = product.CategoryID;
                entity.PriceIn = product.PriceIn;
                entity.PriceOut = product.PriceOut;
                entity.ManufacturerId = product.ManufacturerID;
                entity.Status = product.Status;
                context.SaveChanges();
                return true;
            }
        }
    }
}
