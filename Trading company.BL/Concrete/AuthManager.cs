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
            if (!ok)
            {
                CurrentUser = null;
                CurrentUserChanged?.Invoke();
                return false;
            }

            // Отримуємо користувача з усіма привілеями
            var user = _userDal.GetUserByLogin(username);
            CurrentUser = user;
            CurrentUserChanged?.Invoke();
            return true;
        }

        public bool IsAdmin(User user)
        {
            return user.Privileges != null &&
                   user.Privileges.Any(p => p.Name.Equals("Admin", StringComparison.OrdinalIgnoreCase));
        }

        public void SetCurrentUser(User user)
        {
            CurrentUser = user;
            CurrentUserChanged?.Invoke();
        }

        public User CreateUser(string email, string username, string password, PrivilegeType type)
        {
            return _userDal.CreateUser(email, username, password, type);
        }

        public User GetUserById(int id) => _userDal.GetUserById(id);

        public User GetUserByLogin(string username) => _userDal.GetUserByLogin(username);

        public List<User> GetUsers() => _userDal.GetUsers();
    }
}
