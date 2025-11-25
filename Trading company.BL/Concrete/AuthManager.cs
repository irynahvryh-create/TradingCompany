using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TradingCompany.BL.Interfaces;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.BL.Concrete
{
    public class AuthManager : IAuthManager
    {
        private readonly IUserDal _userDal;
        private readonly IUserPrivilegeDal _userPrivilegeDal;


       

        public AuthManager(IUserDal userDal, IUserPrivilegeDal userPrivilegeDal)
        {
            _userDal = userDal;
            _userPrivilegeDal = userPrivilegeDal;
        }

        public User CreateUser(string email, string username, string password, PrivilegeType privilegeType)
        {
            var user = _userDal.CreateUser(email, username, password);
            if (user == null || user.UserID <= 0)
            {
                throw new Exception("User creation failed.");
            }
            _userPrivilegeDal.AddPrivilegeToUser(user.UserID, privilegeType);

            return _userDal.GetUserById(user.UserID);
        }

        //public string GetConnectionStatusTest()
        //{
        //    try
        //    {
        //        // Викликаємо DAL для перевірки з'єднання
        //        var users = _userDal.GetUsers();

        //        if (users == null || users.Count == 0)
        //        {
        //            return "SUCCESS: Connected to DB. Users table is empty.";
        //        }

        //        return $"SUCCESS: Connected to DB. {users.Count} users found.";
        //    }
        //    catch (Exception ex)
        //    {
        //        // Якщо є помилка підключення
        //        return $"ERROR: Connection failed! Details: {ex.Message}";
        //    }
        //}

        public User GetUserById(int id)
        {
            return _userDal.GetUserById(id);
        }

        public User GetUserByLogin(string username)
        {
            return _userDal.GetUserByLogin(username);
        }

        public List<User> GetUsers()
        {
            return _userDal.GetUsers();
        }

        public bool Login(string username, string password)
        {
            return _userDal.Login(username, password);
        }


       
    }
}
