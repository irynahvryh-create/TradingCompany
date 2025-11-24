
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TradingCompany.DAL.EF.Data;
using TradingCompany.DAL.EF.Models;
using TradingCompany.DAL.Interfaces;



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

        public User CreateUser(string email, string username, string password)
        {
            using (var context = CreateContext())
            {
                if (context.Users.Any(u => u.Login == username))
                {
                    throw new Exception("User already exists!");
                }

                Guid salt = Guid.NewGuid();
                var user = new TradingCompany.DAL.EF.Models.User
                {
                    Login = username,
                    Email = email,
                    Password = Hash(password, salt.ToString()),
                    Salt = salt,
                    RowInsertTime = DateTime.UtcNow,
                    RowUpdateTime = DateTime.UtcNow
                };

                context.Users.Add(user);
                context.SaveChanges();

                return _mapper.Map<User>(user);
            }
        }

        public User GetUserById(int id)
        {
            using (var context = CreateContext())
            {
                var user = context.Users
                    .Include(u => u.UserPrivileges)
                    .ThenInclude(up => up.Privilege)
                    .SingleOrDefault(u => u.UserId == id);

                return _mapper.Map<User>(user);
            }
        }

        public User GetUserByLogin(string username)
        {
            using (var context = CreateContext())
            {
                var user = context.Users
                    .Include(u => u.UserPrivileges)
                    .ThenInclude(up => up.Privilege)
                    .SingleOrDefault(u => u.Login == username);

                return _mapper.Map<User>(user);
            }
        }

        public List<User> GetUsers()
        {
            using (var context = CreateContext())
            {
                var users = context.Users
                    .Include(u => u.UserPrivileges)
                    .ThenInclude(up => up.Privilege)
                    .ToList();

                return _mapper.Map<List<User>>(users);
            }
        }

        public bool Login(string username, string password)
        {
            using (var context = CreateContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Login == username);
                return user != null && user.Password.SequenceEqual(Hash(password, user.Salt.ToString()));
            }
        }

        private byte[] Hash(string password, string salt)
        {
            var alg = SHA512.Create();
            return alg.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
        }


       
    }
}
