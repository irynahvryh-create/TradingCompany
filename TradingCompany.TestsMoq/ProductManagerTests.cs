using Moq;
using Trading_company.BL.Concrete;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.TestsMoq;

[TestFixture]
public class ProductManagerTests
{
    private Mock<IProductDal> _mockDal;
    private ProductManager _manager;

    [SetUp]
    public void Setup()
    {
        _mockDal = new Mock<IProductDal>();
        _manager = new ProductManager(_mockDal.Object);
    }

    [Test]
    public void GetAllProducts_ReturnsList()
    {
        var list = new List<Product> { new Product { ProductID = 1, Name = "P1" } };
        _mockDal.Setup(d => d.GetAll()).Returns(list);

        var result = _manager.GetAllProducts();

        Assert.AreEqual(list, result);
    }

    [Test]
    public void CreateProduct_ReturnsCreated()
    {
        var p = new Product { Name = "NewP" };
        _mockDal.Setup(d => d.Create(p)).Returns(new Product { ProductID = 1, Name = "NewP" });

        var result = _manager.CreateProduct(p);

        Assert.AreEqual(1, result.ProductID);
    }

    [Test]
    public void GetProductById_ReturnsProduct()
    {
        var p = new Product { ProductID = 1, Name = "P1" };
        _mockDal.Setup(d => d.GetById(1)).Returns(p);

        var result = _manager.GetProductById(1);

        Assert.AreEqual(p, result);
    }

    [Test]
    public void UpdateProduct_WhenSuccess_ReturnsUpdated()
    {
        var p = new Product { ProductID = 1, Name = "Updated" };
        _mockDal.Setup(d => d.Update(p)).Returns(true);

        var result = _manager.UpdateProduct(p);

        Assert.AreEqual(p, result);
    }

    [Test]
    public void UpdateProduct_WhenFail_ThrowsException()
    {
        var p = new Product { ProductID = 1, Name = "Updated" };
        _mockDal.Setup(d => d.Update(p)).Returns(false);

        Assert.Throws<System.Exception>(() => _manager.UpdateProduct(p));
    }

    [Test]
    public void DeleteProduct_ReturnsDalResult()
    {
        _mockDal.Setup(d => d.Delete(1)).Returns(true);
        Assert.IsTrue(_manager.DeleteProduct(1));
    }
}
