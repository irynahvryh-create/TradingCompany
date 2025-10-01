using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DAL.EF.Concrete;
using TradingCompany.DAL.EF.Models;
using TradingCompany.DAL.Interfaces;

namespace TradingCompany.DAL.EF.Data
{
    public class ManufactureDalEF : IManufactureDal
    {
        private readonly string _connStr;
        private readonly IMapper _mapper;
        public ManufactureDalEF(string connStr, IMapper mapper)
        {
            _connStr = connStr;
            _mapper = mapper;
        }

        public DTO.Manufacture Create(DTO.Manufacture manufacture)
        {
            using (var context = new TradingCompanyContext())
            {
                var entity = _mapper.Map<Models.Manufacture>(manufacture);
                context.Manufactures.Add(entity);
                context.SaveChanges();
                return _mapper.Map<DTO.Manufacture>(entity);
            }
        }

        public bool Delete(int id)
        {
            using (var context = new TradingCompanyContext())
            {
                var manufacture = context.Manufactures.Find(id);
                if (manufacture == null) return false;
                context.Manufactures.Remove(manufacture);
                context.SaveChanges();
                return true;
            }

            }

        public List<DTO.Manufacture> GetAll()
        {
            using var context = new TradingCompanyContext();
            var list = context.Manufactures
                              .Include(m => m.Products) // якщо потрібно завантажити продукти
                              .ToList();
            return _mapper.Map<List<DTO.Manufacture>>(list);
        }

        public DTO.Manufacture? GetById(int id)
        {
            using (var context = new TradingCompanyContext())
            {
                var manufacture = context.Manufactures
                                         .Include(m => m.Products) // якщо потрібно завантажити продукти
                                         .FirstOrDefault(m => m.ManufacturerId == id);
                return manufacture == null ? null : _mapper.Map<DTO.Manufacture>(manufacture);
            }
        }

        public bool Update(DTO.Manufacture manufacture)
        {
            using (var context = new TradingCompanyContext())
            {
                var existingManufacture = context.Manufactures
                                                    .FirstOrDefault(m => m.ManufacturerId == manufacture.ManufacturerID);
                if (existingManufacture == null) return false;
                _mapper.Map(manufacture, existingManufacture);
                context.SaveChanges();
                return true;
            }
            }
    }
}

