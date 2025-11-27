using Moq;
using NUnit.Framework;
using Trading_company.BL.Concrete;
using Trading_company.BL.Interfaces;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;
using System.Collections.Generic;

namespace TradingCompany.TestsMoq
{
    [TestFixture]
    public class ProductLogManagerTests
    {
        private Mock<IProductLogDal> _mockProductLogDal;
        private ProductLogManager _manager;

        [SetUp]
        public void Setup()
        {
            _mockProductLogDal = new Mock<IProductLogDal>();
            _manager = new ProductLogManager(_mockProductLogDal.Object);
        }

        [Test]
        public void CreateProductLog_ReturnsProductLog()
        {
            var log = new ProductLog { LogID = 1, ProductID = 10, OldPrice = 100, NewPrice = 120 };

            // Налаштовуємо mock
            _mockProductLogDal.Setup(d => d.Create(log)).Returns(log);

            var result = _manager.CreateProductLog(log);

            Assert.AreEqual(log, result);
            _mockProductLogDal.Verify(d => d.Create(log), Times.Once);
        }

        [Test]
        public void DeleteProductLog_CallsDalDelete_ReturnsTrue()
        {
            _mockProductLogDal.Setup(d => d.Delete(1)).Returns(true);

            var result = _manager.DeleteProductLog(1);

            Assert.IsTrue(result);
            _mockProductLogDal.Verify(d => d.Delete(1), Times.Once);
        }

        [Test]
        public void GetAllProductLog_ReturnsList()
        {
            var list = new List<ProductLog>
            {
                new ProductLog { LogID = 1 },
                new ProductLog { LogID = 2 }
            };
            _mockProductLogDal.Setup(d => d.GetAll()).Returns(list);

            var result = _manager.GetAllProductLog();

            Assert.AreEqual(list, result);
            _mockProductLogDal.Verify(d => d.GetAll(), Times.Once);
        }

        [Test]
        public void GetProductLogById_ReturnsProductLog()
        {
            var log = new ProductLog { LogID = 1 };
            _mockProductLogDal.Setup(d => d.GetById(1)).Returns(log);

            var result = _manager.GetProductLogById(1);

            Assert.AreEqual(log, result);
            _mockProductLogDal.Verify(d => d.GetById(1), Times.Once);
        }

        [Test]
        public void UpdateProductLog_ReturnsUpdatedLog_WhenDalUpdateTrue()
        {
            var log = new ProductLog { LogID = 1, ProductID = 10 };
            _mockProductLogDal.Setup(d => d.Update(log)).Returns(true);

            var result = _manager.UpdateProductLog(log);

            Assert.AreEqual(log, result);
            _mockProductLogDal.Verify(d => d.Update(log), Times.Once);
        }

        [Test]
        public void UpdateProductLog_ThrowsException_WhenDalUpdateFails()
        {
            var log = new ProductLog { LogID = 1, ProductID = 10 };
            _mockProductLogDal.Setup(d => d.Update(log)).Returns(false);

            Assert.Throws<System.Exception>(() => _manager.UpdateProductLog(log));
            _mockProductLogDal.Verify(d => d.Update(log), Times.Once);
        }
    }
}
