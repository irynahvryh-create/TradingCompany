using Moq;
using Trading_company.BL.Concrete;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.TestsMoq;

[TestFixture]
public class ManufactureManagerTests
{
    private Mock<IManufactureDal> _mockDal;
    private ManufactureManager _manager;

    [SetUp]
    public void Setup()
    {
        _mockDal = new Mock<IManufactureDal>();
        _manager = new ManufactureManager(_mockDal.Object);
    }

    [Test]
    public void GetAllManufactures_ReturnsList()
    {
        var list = new List<Manufacture> { new Manufacture { ManufacturerID = 1, Name = "M1" } };
        _mockDal.Setup(d => d.GetAll()).Returns(list);

        var result = _manager.GetAllManufactures();

        Assert.AreEqual(list, result);
    }

    [Test]
    public void CreateManufacture_ReturnsCreated()
    {
        var m = new Manufacture { Name = "NewM" };
        _mockDal.Setup(d => d.Create(m)).Returns(new Manufacture { ManufacturerID = 1, Name = "NewM" });

        var result = _manager.CreateManufacture(m);

        Assert.AreEqual(1, result.ManufacturerID);
    }

    [Test]
    public void GetManufactureById_ReturnsManufacture()
    {
        var m = new Manufacture { ManufacturerID = 1, Name = "M1" };
        _mockDal.Setup(d => d.GetById(1)).Returns(m);

        var result = _manager.GetManufactureById(1);

        Assert.AreEqual(m, result);
    }

    [Test]
    public void UpdateManufacture_WhenSuccess_ReturnsUpdated()
    {
        var m = new Manufacture { ManufacturerID = 1, Name = "Updated" };
        _mockDal.Setup(d => d.Update(m)).Returns(true);

        var result = _manager.UpdateManufacture(m);

        Assert.AreEqual(m, result);
    }

    [Test]
    public void UpdateManufacture_WhenFail_ThrowsException()
    {
        var m = new Manufacture { ManufacturerID = 1, Name = "Updated" };
        _mockDal.Setup(d => d.Update(m)).Returns(false);

        Assert.Throws<System.Exception>(() => _manager.UpdateManufacture(m));
    }

    [Test]
    public void DeleteManufacture_ReturnsDalResult()
    {
        _mockDal.Setup(d => d.Delete(1)).Returns(true);
        Assert.IsTrue(_manager.DeleteManufacture(1));
    }
}


