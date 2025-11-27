using Moq;
using NUnit.Framework;
using TradingCompany.BL.Concrete;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;
using System.Collections.Generic;

namespace TradingCompany.TestsMoq
{
    [TestFixture]
    public class AuthManagerTests
    {
        private Mock<IUserDal> _mockUserDal;
        private Mock<IUserPrivilegeDal> _mockPrivilegeDal;
        private AuthManager _manager;

        [SetUp]
        public void Setup()
        {
            _mockUserDal = new Mock<IUserDal>();
            _mockPrivilegeDal = new Mock<IUserPrivilegeDal>();
            _manager = new AuthManager(_mockUserDal.Object, _mockPrivilegeDal.Object);
        }

        [Test]
        public void CreateUser_ReturnsCreatedUser()
        {
            var user = new User { UserID = 1, Login = "test" };

            _mockUserDal.Setup(d => d.CreateUser("a@b.com", "test", "pass", PrivilegeType.Admin))
                        .Returns(user);

            var result = _manager.CreateUser("a@b.com", "test", "pass", PrivilegeType.Admin);

            Assert.AreEqual(user, result);
        }

        [Test]
        public void Login_ReturnsTrue_WhenCredentialsAreCorrect()
        {
            var user = new User { UserID = 1, Login = "user" };

            _mockUserDal.Setup(d => d.Login("user", "pass")).Returns(true);
            _mockUserDal.Setup(d => d.GetUserByLogin("user")).Returns(user);

            var result = _manager.Login("user", "pass");

            Assert.IsTrue(result);
            Assert.AreEqual(user, _manager.CurrentUser);
        }

        [Test]
        public void Login_ReturnsFalse_WhenCredentialsAreWrong()
        {
            _mockUserDal.Setup(d => d.Login("user", "wrongpass")).Returns(false);

            var result = _manager.Login("user", "wrongpass");

            Assert.IsFalse(result);
            Assert.IsNull(_manager.CurrentUser);
        }

        [Test]
        public void GetUserById_ReturnsUser()
        {
            var user = new User { UserID = 1, Login = "user" };
            _mockUserDal.Setup(d => d.GetUserById(1)).Returns(user);

            var result = _manager.GetUserById(1);

            Assert.AreEqual(user, result);
        }

        [Test]
        public void GetUserByLogin_ReturnsUser()
        {
            var user = new User { UserID = 1, Login = "user" };
            _mockUserDal.Setup(d => d.GetUserByLogin("user")).Returns(user);

            var result = _manager.GetUserByLogin("user");

            Assert.AreEqual(user, result);
        }

        [Test]
        public void GetUsers_ReturnsList()
        {
            var list = new List<User> { new User { UserID = 1, Login = "user1" } };
            _mockUserDal.Setup(d => d.GetUsers()).Returns(list);

            var result = _manager.GetUsers();

            Assert.AreEqual(list, result);
        }

        [Test]
        public void IsAdmin_ReturnsTrue_WhenUserHasAdminPrivilege()
        {
            var user = new User
            {
                UserID = 1,
                Login = "admin",
                Privileges = new List<Privilege> { new Privilege { Name = "Admin" } }
            };

            Assert.IsTrue(_manager.IsAdmin(user));
        }

        [Test]
        public void IsAdmin_ReturnsFalse_WhenUserHasNoAdminPrivilege()
        {
            var user = new User
            {
                UserID = 2,
                Login = "user",
                Privileges = new List<Privilege> { new Privilege { Name = "User" } }
            };

            Assert.IsFalse(_manager.IsAdmin(user));
        }
    }
}
