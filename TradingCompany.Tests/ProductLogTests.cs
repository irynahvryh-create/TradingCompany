using AutoMapper;
using TradingCompany.DAL.EF.Models;
using TradingCompany.DTO;
using NUnit.Framework;
using TradingCompany.DAL.EF.Concrete;

namespace TradingCompany.Tests;

[TestFixture]
public class ProductLogTests
{
    private readonly string _connStr = "Data Source=localhost;Initial Catalog=TradingCompany_Test;Integrated Security=True;Trust Server Certificate=True";
    private ProductLogDalEF _dalEF;
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        // Налаштування AutoMapper
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<DAL.EF.Models.ProductLog, DTO.ProductLog>().ReverseMap();
        });
        _mapper = config.CreateMapper();

        // Ініціалізація DAL для тестової бази
        _dalEF = new ProductLogDalEF(_connStr, _mapper);
    }

    [Test]
    public void GetAll_ShouldReturnListOfProductLogs()
    {
        var logs = _dalEF.GetAll();
        Assert.IsNotNull(logs);
        Assert.IsInstanceOf<List<DTO.ProductLog>>(logs);
    }

    [Test]
    public void Create_ShouldAddNewProductLog()
    {
        var newLog = new DTO.ProductLog
        {
            ProductID = 1,
            OldPrice = 10m,
            NewPrice = 12m,
            Status = true,
            Comment = "Test log",
            Date = DateTime.Now
        };

        var created = _dalEF.Create(newLog);

        Assert.IsNotNull(created);
        Assert.Greater(created.LogID, 0);
        Assert.AreEqual("Test log", created.Comment);

        _dalEF.Delete(created.LogID);
    }

    [Test]
    public void GetById_ShouldReturnProductLog()
    {
        var newLog = new DTO.ProductLog
        {
            ProductID = 1,
            OldPrice = 5m,
            NewPrice = 6m,
            Status = true,
            Comment = "Temp log",
            Date = DateTime.Now
        };
        var created = _dalEF.Create(newLog);

        var fetched = _dalEF.GetById(created.LogID);
        Assert.IsNotNull(fetched);
        Assert.AreEqual(created.LogID, fetched.LogID);

        _dalEF.Delete(created.LogID);
    }

    [Test]
    public void GetById_NonExisting_ShouldReturnNull()
    {
        var fetched = _dalEF.GetById(-999); // ID, якого немає
        Assert.IsNull(fetched);
    }

    [Test]
    public void Update_ShouldModifyProductLog()
    {
        var newLog = new DTO.ProductLog
        {
            ProductID = 1,
            OldPrice = 10m,
            NewPrice = 15m,
            Status = true,
            Comment = "Old comment",
            Date = DateTime.Now
        };
        var created = _dalEF.Create(newLog);

        // Зміни для тесту
        created.NewPrice = 20m;
        created.Comment = "Updated comment";
        created.Status = false;

        var result = _dalEF.Update(created);
        Assert.IsTrue(result);

        var updated = _dalEF.GetById(created.LogID);
        Assert.AreEqual(20m, updated.NewPrice);
        Assert.AreEqual("Updated comment", updated.Comment);
        Assert.AreEqual(false, updated.Status);

        _dalEF.Delete(created.LogID);
    }

    [Test]
    public void Delete_ShouldRemoveProductLog()
    {
        var newLog = new DTO.ProductLog
        {
            ProductID = 1,
            OldPrice = 3m,
            NewPrice = 5m,
            Status = true,
            Comment = "To Be Deleted",
            Date = DateTime.Now
        };
        var created = _dalEF.Create(newLog);

        var deleted = _dalEF.Delete(created.LogID);
        Assert.IsTrue(deleted);

        var fetched = _dalEF.GetById(created.LogID);
        Assert.IsNull(fetched);
    }

    [Test]
    public void Delete_NonExisting_ShouldReturnFalse()
    {
        var result = _dalEF.Delete(-999); // ID, якого немає
        Assert.IsFalse(result);
    }
}
