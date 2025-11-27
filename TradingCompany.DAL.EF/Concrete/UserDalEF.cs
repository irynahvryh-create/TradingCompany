using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TradingCompany.DAL.EF.Data;
using TradingCompany.DAL.EF.Models;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;
using User = TradingCompany.DTO.User;

namespace TradingCompany.DAL.EF.Concrete
{
    public class UserDalEF : IUserDal
    {
        private readonly string _connStr;
        private readonly IMapper _mapper;

        public UserDalEF(string connStr, IMapper mapper)
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

        public User CreateUser(string email, string username, string password, PrivilegeType privilegeType)
        {
            using var context = CreateContext();

            if (context.Users.Any(u => u.Login == username))
                throw new Exception("User already exists!");

            var salt = Guid.NewGuid();
            var user = new Models.User
            {
                Login = username,
                Email = email,
                Password = HashPassword(password, salt.ToString()),
                Salt = salt,
                RowInsertTime = DateTime.UtcNow,
                RowUpdateTime = DateTime.UtcNow
            };
            context.Users.Add(user);
            context.SaveChanges(); // щоб згенерувався UserId

            // Створюємо або отримуємо привілею
            var privilegeName = privilegeType.ToString();
            var privilege = context.Privileges.SingleOrDefault(p => p.Name == privilegeName);
            if (privilege == null)
            {
                privilege = new Models.Privilege
                {
                    Name = privilegeName,
                    RowInsertTime = DateTime.UtcNow
                };
                context.Privileges.Add(privilege);
                context.SaveChanges();
            }

            // Прив'язуємо користувача до привілеї
            context.UserPrivileges.Add(new UserPrivilege
            {
                UserId = user.UserId,
                PrivilegeId = privilege.PrivilegeId,
                RowInsertTime = DateTime.UtcNow
            });
            context.SaveChanges();

            // Повертаємо DTO з усіма привілеями
            return GetUserById(user.UserId);
        }

        public User GetUserById(int id)
        {
            using var context = CreateContext();
            var user = context.Users
                .Include(u => u.UserPrivileges)
                .ThenInclude(up => up.Privilege)
                .SingleOrDefault(u => u.UserId == id);

            return _mapper.Map<User>(user);
        }

        public User GetUserByLogin(string username)
        {
            using var context = CreateContext();
            var user = context.Users
                .Include(u => u.UserPrivileges)
                .ThenInclude(up => up.Privilege)
                .SingleOrDefault(u => u.Login == username);

            return _mapper.Map<User>(user);
        }

        public List<User> GetUsers()
        {
            using var context = CreateContext();
            var users = context.Users
                .Include(u => u.UserPrivileges)
                .ThenInclude(up => up.Privilege)
                .ToList();

            return _mapper.Map<List<User>>(users);
        }

        public bool Login(string username, string password)
        {
            using var context = CreateContext();
            var user = context.Users
                .Include(u => u.UserPrivileges)
                .ThenInclude(up => up.Privilege)
                .SingleOrDefault(u => u.Login == username);

            if (user == null) return false;
            return user.Password.SequenceEqual(HashPassword(password, user.Salt.ToString()));
        }

        private byte[] HashPassword(string password, string salt)
        {
            using var alg = SHA512.Create();
            return alg.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
        }
    }
}
