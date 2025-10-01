using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.DAL.EF.Data
{
    public class CategoryDalEF : ICategoryDal
    {
        private readonly string _connStr;
        private readonly IMapper _mapper;
        public CategoryDalEF(string connStr, IMapper mapper)
        {
            _connStr = connStr;
            _mapper = mapper;
        }

        public Category Create(Category category)
        {
           using (var context = new TradingCompanyContext())
            {
                var entity = _mapper.Map<TradingCompany.DAL.EF.Models.Category>(category);
                context.Categories.Add(entity);
                context.SaveChanges();
                category.CategoryID = entity.CategoryId;
                return category;
            }
        }

        public bool Delete(int id)
        {
           using (var context = new TradingCompanyContext())
            {
                var category = context.Categories.Find(id);
                if (category == null) return false;
                context.Categories.Remove(category);
                context.SaveChanges();
                return true;
            }
        }

        public List<Category> GetAll()
        {
            using (var context = new TradingCompanyContext())
            {
                return context.Categories.Select(
                    m=> new  Category
                    {
                        CategoryID = m.CategoryId,
                        Name = m.Name,
                        Status = m.Status
                    }).ToList();
            }
        }

        public Category? GetById(int id)
        {
            using (var context = new TradingCompanyContext())
            {
                var c= context.Categories.
                    Include(c=>c.Products).
              
                    FirstOrDefault(c=>c.CategoryId==id);
                // var category = context.Categories.Find(id);
                if (c == null) return null;
                return _mapper.Map<Category>(c);
            }
        }

        public bool Update(Category category)
        {
            using (var context = new TradingCompanyContext())
            {
                var existingCategory = context.Categories.Find(category.CategoryID);
                if (existingCategory == null) return false;
                existingCategory.Name = category.Name;
                existingCategory.Status = category.Status;
                context.SaveChanges();
                return true;
            }
        }
    }
}
