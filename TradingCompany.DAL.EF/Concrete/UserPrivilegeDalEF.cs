using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DAL.EF.Data;
using TradingCompany.DAL.EF.Models;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.DAL.EF.Concrete
{
    public class UserPrivilegeDalEF : IUserPrivilegeDal
    {
        private readonly string _connStr;
        private readonly IMapper _mapper;

        public UserPrivilegeDalEF(string connStr, IMapper mapper)
        {
            _connStr = connStr;
            _mapper = mapper;
        }
        private TradingCompanyContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<TradingCompanyContext>()
                .UseSqlServer(_connStr)
                .Options;

            return new TradingCompanyContext(options);
        }

        public void AddPrivilegeToUser(int userId, PrivilegeType privilegeType)
        {
            using (var context = CreateContext())
            {
                var privilege = context.Privileges
                    .SingleOrDefault(p => p.Name == privilegeType.ToString());

                if (privilege == null)
                {
                    throw new Exception($"Privilege '{privilegeType}' not found.");
                }

                var userPrivilege = new UserPrivilege
                {
                    UserId = userId,
                    PrivilegeId = privilege.PrivilegeId,
                    RowInsertTime = DateTime.UtcNow 
                };

                context.UserPrivileges.Add(userPrivilege);
                context.SaveChanges();

                if (userPrivilege.RowInsertTime == default)
                {
                    throw new Exception("Failed to add privilege to user.");
                }
            }
        }
    }
}
