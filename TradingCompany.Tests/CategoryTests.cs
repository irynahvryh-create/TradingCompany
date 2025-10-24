using AutoMapper;
using TradingCompany.DAL.EF.Data;
using TradingCompany.DAL.EF.Models;
using TradingCompany.DTO;
using NUnit.Framework;

namespace TradingCompany.Tests;

[TestFixture]
public class CategoryTests
{
    private readonly string _connStr = "Data Source=localhost;Initial Catalog=TradingCompany_Test;Integrated Security=True;Trust Server Certificate=True";
    private CategoryDalEF _dalEF;
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        // Налаштування AutoMapper
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<DAL.EF.Models.Category, DTO.Category>().ReverseMap();
        });
        _mapper = config.CreateMapper();

        // Ініціалізація DAL для тестової бази
        _dalEF = new CategoryDalEF(_connStr, _mapper);
    }

    [Test]
    public void GetAll_ShouldReturnListOfCategories()
    {
        var categories = _dalEF.GetAll();
        Assert.IsNotNull(categories);
        Assert.IsInstanceOf<List<DTO.Category>>(categories);
    }

    [Test]
    public void Create_ShouldAddNewCategory()
    {
        var newCategory = new DTO.Category
        {
            Name = "Test Category",
            Status = true
        };

        var created = _dalEF.Create(newCategory);

        Assert.IsNotNull(created);
        Assert.Greater(created.CategoryID, 0);
        Assert.AreEqual("Test Category", created.Name);

        _dalEF.Delete(created.CategoryID);
    }

    [Test]
    public void GetById_ShouldReturnCategory()
    {
        var newCategory = new DTO.Category
        {
            Name = "Temp Category",
            Status = true
        };
        var created = _dalEF.Create(newCategory);

        var fetched = _dalEF.GetById(created.CategoryID);
        Assert.IsNotNull(fetched);
        Assert.AreEqual(created.CategoryID, fetched.CategoryID);

        _dalEF.Delete(created.CategoryID);
    }

    [Test]
    public void Update_ShouldModifyCategory()
    {
        var newCategory = new DTO.Category
        {
            Name = "Old Name",
            Status = true
        };
        var created = _dalEF.Create(newCategory);

        // Зміни для тесту
        created.Name = "Updated Name";
        created.Status = false;

        var result = _dalEF.Update(created);
        Assert.IsTrue(result);

        var updated = _dalEF.GetById(created.CategoryID);
        Assert.AreEqual("Updated Name", updated.Name);
        Assert.AreEqual(false, updated.Status);

        _dalEF.Delete(created.CategoryID);
    }

    [Test]
    public void Delete_ShouldRemoveCategory()
    {
        var newCategory = new DTO.Category
        {
            Name = "To Be Deleted",
            Status = true
        };
        var created = _dalEF.Create(newCategory);

        var deleted = _dalEF.Delete(created.CategoryID);
        Assert.IsTrue(deleted);

        var fetched = _dalEF.GetById(created.CategoryID);
        Assert.IsNull(fetched); // Перевіряємо, що записи більше немає
    }

    [Test]
    public void Delete_NonExisting_ShouldReturnFalse()
    {
        var result = _dalEF.Delete(-999); 
        Assert.IsFalse(result);
    }
}
