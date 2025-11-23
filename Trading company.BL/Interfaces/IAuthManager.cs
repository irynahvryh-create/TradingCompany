using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DTO;

namespace TradingCompany.BL.Interfaces
{
    public interface IAuthManager
    {
        
        bool Login(string username, string password);
        User CreateUser(string email, string username, string password, PrivilegeType privilegeType);
        User GetUserByLogin(string username);
        User GetUserById(int id);
        List<User> GetUsers();

        string GetConnectionStatusTest();
    }
}
