﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.DAL.EF.Data
{
    public class ProductLogDalEF : IProductLogDal
    {
        private readonly string _connStr;
        private readonly IMapper _mapper;
        public ProductLogDalEF(string connStr, IMapper mapper)
        {
            _connStr = connStr;
            _mapper = mapper;
        }

        public ProductLog Create(ProductLog log)
        {
            using (var context = new TradingCompanyContext())
            {
                var entity = _mapper.Map<Models.ProductLog>(log);
                context.ProductLogs.Add(entity);
                context.SaveChanges();
                return _mapper.Map<ProductLog>(entity);
            }
        }

        public bool Delete(int id)
        {
            using (var context = new TradingCompanyContext())
            {
                var entity = context.ProductLogs.Find(id);
                if (entity == null)
                    return false;
                context.ProductLogs.Remove(entity);
                context.SaveChanges();
                return true;
            }
        }

        public List<ProductLog> GetAll()
        {
            using (var context = new TradingCompanyContext())
            {
                var entities = context.ProductLogs.ToList();
                return _mapper.Map<List<ProductLog>>(entities);
            }
        }

        public ProductLog? GetById(int id)
        {
            using (var context = new TradingCompanyContext())
            {
                var entity = context.ProductLogs.Find(id);
                return entity == null ? null : _mapper.Map<ProductLog>(entity);
            }
        }

        public List<ProductLog> GetByProductId(int productId)
        {
            using (var context = new TradingCompanyContext())
            {
                var entities = context.ProductLogs.Where(pl => pl.ProductId == productId).ToList();
                return _mapper.Map<List<ProductLog>>(entities);
            }
        }

        public bool Update(ProductLog log)
        {
            using (var context = new TradingCompanyContext())
            {
                var entity = context.ProductLogs.Find(log.LogID);
                if (entity == null)
                    return false;
                _mapper.Map(log, entity);
                context.SaveChanges();
                return true;
            }
        }
    }
}
