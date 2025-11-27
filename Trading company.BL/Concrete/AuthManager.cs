using System;
using System.Linq;
using System.Collections.Generic;
using TradingCompany.BL.Interfaces;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.BL.Concrete
{
    public class AuthManager : IAuthManager
    {
        private readonly IUserDal _userDal;
        private readonly IUserPrivilegeDal _userPrivilegeDal;

        public User? CurrentUser { get; private set; }
        public event Action? CurrentUserChanged;

        public AuthManager(IUserDal userDal, IUserPrivilegeDal userPrivilegeDal)
        {
            _userDal = userDal;
            _userPrivilegeDal = userPrivilegeDal;
        }

        public bool Login(string username, string password)
        {
            bool ok = _userDal.Login(username, password);
            if (!ok) return false;

            CurrentUser = _userDal.GetUserByLogin(username);
            CurrentUserChanged?.Invoke();
            return true;
        }

        public bool IsAdmin(User user)
        {
            return user.Privileges.Any(p => p.Name == "Admin");
        }

        public void SetCurrentUser(User user)
        {
            CurrentUser = user;
            CurrentUserChanged?.Invoke();
        }

        public User CreateUser(string email, string username, string password, PrivilegeType type)
        {
            var user = _userDal.CreateUser(email, username, password);
            _userPrivilegeDal.AddPrivilegeToUser(user.UserID, type);
            return _userDal.GetUserById(user.UserID);
        }

        public User GetUserById(int id) => _userDal.GetUserById(id);
        public User GetUserByLogin(string username) => _userDal.GetUserByLogin(username);
        public List<User> GetUsers() => _userDal.GetUsers();
    }
}
