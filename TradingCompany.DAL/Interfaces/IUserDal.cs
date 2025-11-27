using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DTO;

namespace TradingCompany.DAL.Interfaces
{
    public interface IUserDal
    {
        User CreateUser(string email, string username, string password, PrivilegeType privilegeType);

        User GetUserById(int id);
        User GetUserByLogin(string username);
        List<User> GetUsers();
        bool Login(string username, string password);
    }
}
