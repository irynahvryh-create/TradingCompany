using System;
using System.Collections.Generic;
using TradingCompany.DTO;

namespace TradingCompany.BL.Interfaces
{
    public interface IAuthManager
    {
        bool Login(string username, string password);

        User CreateUser(string email, string username, string password, PrivilegeType privilegeType);

        User? CurrentUser { get; }
        bool IsAdmin(User user);
        event Action? CurrentUserChanged;

        User GetUserByLogin(string username);
        User GetUserById(int id);
        List<User> GetUsers();

        void SetCurrentUser(User user);
    }
}
