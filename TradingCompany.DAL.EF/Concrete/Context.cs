using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DAL.EF.Data;

namespace TradingCompany.DAL.EF.Concrete
{
    public class Context : TradingCompanyContext
    {

        private readonly string _connStr;
        public Context(string connStr)
        {
            _connStr = connStr;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     => optionsBuilder.UseSqlServer(_connStr);


    }
}
