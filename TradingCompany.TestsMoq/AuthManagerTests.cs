using Moq;
using TradingCompany.BL.Concrete;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.TestsMoq;

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
    public void CreateUser_AddsUserAndPrivilege()
    {
        var user = new User { UserID = 1, Login = "test" };
        _mockUserDal.Setup(d => d.CreateUser("a@b.com", "test", "pass")).Returns(user);
        _mockUserDal.Setup(d => d.GetUserById(1)).Returns(user);

        var result = _manager.CreateUser("a@b.com", "test", "pass", PrivilegeType.Admin);

        Assert.AreEqual(user, result);
        _mockPrivilegeDal.Verify(p => p.AddPrivilegeToUser(1, PrivilegeType.Admin), Times.Once);
    }

    [Test]
    public void Login_ReturnsTrue_WhenDalReturnsTrue()
    {
        _mockUserDal.Setup(d => d.Login("user", "pass")).Returns(true);
        Assert.IsTrue(_manager.Login("user", "pass"));
    }

    [Test]
    public void GetUserById_ReturnsUser()
    {
        var user = new User { UserID = 1, Login = "user" };
        _mockUserDal.Setup(d => d.GetUserById(1)).Returns(user);
        Assert.AreEqual(user, _manager.GetUserById(1));
    }

    [Test]
    public void GetUserByLogin_ReturnsUser()
    {
        var user = new User { UserID = 1, Login = "user" };
        _mockUserDal.Setup(d => d.GetUserByLogin("user")).Returns(user);
        Assert.AreEqual(user, _manager.GetUserByLogin("user"));
    }

    [Test]
    public void GetUsers_ReturnsList()
    {
        var list = new List<User> { new User { UserID = 1, Login = "user1" } };
        _mockUserDal.Setup(d => d.GetUsers()).Returns(list);
        Assert.AreEqual(list, _manager.GetUsers());
    }
}
