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

        // У TradingCompany.BL.Concrete/AuthManager.cs

        public bool Login(string username, string password)
        {
            // 1. ПЕРЕВІРКА ПАРОЛЯ
            bool ok = _userDal.Login(username, password);
            if (!ok)
            {
                CurrentUser = null;
                CurrentUserChanged?.Invoke();
                return false;
            }

            // 2. ОТРИМУЄМО ПОВНИЙ ОБ'ЄКТ (якщо автентифікація успішна)
            var user = _userDal.GetUserByLogin(username);

            // 🛑 КРИТИЧНА ПЕРЕВІРКА: Якщо DAL не повернув об'єкт User DTO (може бути помилка мапінгу або БД)
            if (user == null)
            {
                // Це означає, що DAL не зміг знайти або створити DTO
                CurrentUser = null;
                CurrentUserChanged?.Invoke();
                return false;
            }

            // 3. ЗБЕРІГАННЯ СЕСІЇ У BL (АВТОРИЗАЦІЯ)
            CurrentUser = user; // ✅ Успішно встановлюємо об'єкт User DTO
            CurrentUserChanged?.Invoke();
            return true;
        }

        public bool IsAdmin(User user)
        {
            return user.Privileges.Any(p => string.Equals(p.Name, "Admin", StringComparison.OrdinalIgnoreCase));
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
