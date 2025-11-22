using AutoMapper;
using TradingCompany.DAL.EF.Models;
using TradingCompany.DTO;
using NUnit.Framework;
using TradingCompany.DAL.EF.Concrete;

namespace TradingCompany.Tests;

[TestFixture]
public class ProductTests
{
    private readonly string _connStr = "Data Source=localhost;Initial Catalog=TradingCompany_Test;Integrated Security=True;Trust Server Certificate=True";
    private ProductDalEF _dalEF;
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        // Налаштування AutoMapper
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<DAL.EF.Models.Product, DTO.Product>().ReverseMap();
        });
        _mapper = config.CreateMapper();

        // Ініціалізація DAL для тестової бази
        _dalEF = new ProductDalEF(_connStr, _mapper);
    }

    [Test]
    public void GetAll_ShouldReturnListOfProducts()
    {
        var products = _dalEF.GetAll();
        Assert.IsNotNull(products);
        Assert.IsInstanceOf<List<DTO.Product>>(products);
    }

    [Test]
    public void Create_ShouldAddNewProduct()
    {
        var newProduct = new DTO.Product
        {
            Name = "Test Product",
            CategoryID = 1,
            ManufacturerID = 1,
            PriceIn = 10m,
            PriceOut = 15m,
            Status = true
        };

        var created = _dalEF.Create(newProduct);

        Assert.IsNotNull(created);
        Assert.Greater(created.ProductID, 0);
        Assert.AreEqual("Test Product", created.Name);

        _dalEF.Delete(created.ProductID);
    }

    [Test]
    public void GetById_ShouldReturnProduct()
    {
        var newProduct = new DTO.Product
        {
            Name = "Temp Product",
            CategoryID = 1,
            ManufacturerID = 1,
            PriceIn = 5m,
            PriceOut = 8m,
            Status = true
        };
        var created = _dalEF.Create(newProduct);

        var fetched = _dalEF.GetById(created.ProductID);
        Assert.IsNotNull(fetched);
        Assert.AreEqual(created.ProductID, fetched.ProductID);

        _dalEF.Delete(created.ProductID);
    }

    [Test]
    public void GetById_NonExisting_ShouldReturnNull()
    {
        var fetched = _dalEF.GetById(-999); // ID, якого немає
        Assert.IsNull(fetched);
    }

    [Test]
    public void Update_ShouldModifyProduct()
    {
        var newProduct = new DTO.Product
        {
            Name = "Old Product",
            CategoryID = 1,
            ManufacturerID = 1,
            PriceIn = 5m,
            PriceOut = 10m,
            Status = true
        };
        var created = _dalEF.Create(newProduct);

        // Зміни для тесту
        created.Name = "Updated Product";
        created.PriceOut = 12m;
        created.Status = false;

        var result = _dalEF.Update(created);
        Assert.IsTrue(result);

        var updated = _dalEF.GetById(created.ProductID);
        Assert.AreEqual("Updated Product", updated.Name);
        Assert.AreEqual(12m, updated.PriceOut);
        Assert.AreEqual(false, updated.Status);

        _dalEF.Delete(created.ProductID);
    }

    [Test]
    public void Update_NonExisting_ShouldReturnFalse()
    {
        var fakeProduct = new DTO.Product
        {
            ProductID = -999,
            Name = "Fake",
            CategoryID = 1,
            ManufacturerID = 1,
            PriceIn = 1m,
            PriceOut = 2m,
            Status = true
        };

        var result = _dalEF.Update(fakeProduct);
        Assert.IsFalse(result);
    }

    [Test]
    public void Delete_ShouldRemoveProduct()
    {
        var newProduct = new DTO.Product
        {
            Name = "To Be Deleted",
            CategoryID = 1,
            ManufacturerID = 1,
            PriceIn = 2m,
            PriceOut = 4m,
            Status = true
        };
        var created = _dalEF.Create(newProduct);

        var deleted = _dalEF.Delete(created.ProductID);
        Assert.IsTrue(deleted);

        var fetched = _dalEF.GetById(created.ProductID);
        Assert.IsNull(fetched);
    }

    [Test]
    public void Delete_NonExisting_ShouldReturnFalse()
    {
        var result = _dalEF.Delete(-999); // ID, якого немає
        Assert.IsFalse(result);
    }
}
