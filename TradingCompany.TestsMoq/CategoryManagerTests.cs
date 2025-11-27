using Moq;
using TradingCompany.BL.Concrete;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.TestsMoq;

[TestFixture]
public class CategoryManagerTests
{
    private Mock<ICategoryDal> _mockDal;
    private CategoryManager _manager;

    [SetUp]
    public void Setup()
    {
        _mockDal = new Mock<ICategoryDal>();
        _manager = new CategoryManager(_mockDal.Object);
    }

    [Test]
    public void GetAllCategories_ReturnsList()
    {
        var categories = new List<Category> { new Category { CategoryID = 1, Name = "Cat1" } };
        _mockDal.Setup(d => d.GetAll()).Returns(categories);

        var result = _manager.GetAllCategories();

        Assert.AreEqual(categories, result);
    }

    [Test]
    public void CreateCategory_ReturnsCreatedCategory()
    {
        var category = new Category { Name = "NewCat" };
        _mockDal.Setup(d => d.Create(category)).Returns(new Category { CategoryID = 1, Name = "NewCat" });

        var result = _manager.CreateCategory(category);

        Assert.AreEqual(1, result.CategoryID);
        _mockDal.Verify(d => d.Create(category), Times.Once);
    }

    [Test]
    public void GetCategoryById_ReturnsCategory()
    {
        var category = new Category { CategoryID = 1, Name = "Cat1" };
        _mockDal.Setup(d => d.GetById(1)).Returns(category);

        var result = _manager.GetCategoryById(1);

        Assert.AreEqual(category, result);
    }

    [Test]
    public void UpdateCategory_WhenSuccess_ReturnsCategory()
    {
        var category = new Category { CategoryID = 1, Name = "Updated" };
        _mockDal.Setup(d => d.Update(category)).Returns(true);

        var result = _manager.UpdateCategory(category);

        Assert.AreEqual(category, result);
    }

    [Test]
    public void UpdateCategory_WhenFail_ThrowsException()
    {
        var category = new Category { CategoryID = 1, Name = "Updated" };
        _mockDal.Setup(d => d.Update(category)).Returns(false);

        Assert.Throws<System.Exception>(() => _manager.UpdateCategory(category));
    }

    [Test]
    public void DeleteCategory_ReturnsDalResult()
    {
        _mockDal.Setup(d => d.Delete(1)).Returns(true);
        var result = _manager.DeleteCategory(1);
        Assert.IsTrue(result);
    }

}
