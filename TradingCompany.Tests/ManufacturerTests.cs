using AutoMapper;
using TradingCompany.DAL.EF.Data;
using TradingCompany.DAL.EF.Models;
using TradingCompany.DTO;
using NUnit.Framework;

namespace TradingCompany.Tests
{
    [TestFixture]
    public class ManufacturerTests
    {
        private readonly string _connStr = "Data Source=localhost;Initial Catalog=TradingCompany_Test;Integrated Security=True;Trust Server Certificate=True";
        private ManufactureDalEF _dalEF;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            // Налаштування AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DAL.EF.Models.Manufacture, DTO.Manufacture>().ReverseMap();
            });
            _mapper = config.CreateMapper();

            // Ініціалізація DAL для тестової бази
            _dalEF = new ManufactureDalEF(_connStr, _mapper);
        }

        [Test]
        public void GetAll_ShouldReturnListOfManufacturers()
        {
            var manufacturers = _dalEF.GetAll();
            Assert.IsNotNull(manufacturers);
            Assert.IsInstanceOf<List<DTO.Manufacture>>(manufacturers);
        }

        [Test]
        public void Create_ShouldAddNewManufacturer()
        {
            var newManufacturer = new DTO.Manufacture
            {
                Name = "Test Manufacturer",
                Country = "Testland"
            };

            var created = _dalEF.Create(newManufacturer);

            Assert.IsNotNull(created);
            Assert.Greater(created.ManufacturerID, 0);
            Assert.AreEqual("Test Manufacturer", created.Name);

            _dalEF.Delete(created.ManufacturerID);
        }

        [Test]
        public void GetById_ShouldReturnManufacturer()
        {
            var newManufacturer = new DTO.Manufacture
            {
                Name = "Temp Manufacturer",
                Country = "Templand"
            };
            var created = _dalEF.Create(newManufacturer);

            var fetched = _dalEF.GetById(created.ManufacturerID);
            Assert.IsNotNull(fetched);
            Assert.AreEqual(created.ManufacturerID, fetched.ManufacturerID);

            _dalEF.Delete(created.ManufacturerID);
        }

        [Test]
        public void Update_ShouldModifyManufacturer()
        {
            var newManufacturer = new DTO.Manufacture
            {
                Name = "Old Name",
                Country = "Oldland"
            };
            var created = _dalEF.Create(newManufacturer);

            // Зміни для тесту
            created.Name = "Updated Name";
            created.Country = "Updatedland";

            var result = _dalEF.Update(created);
            Assert.IsTrue(result);

            var updated = _dalEF.GetById(created.ManufacturerID);
            Assert.AreEqual("Updated Name", updated.Name);
            Assert.AreEqual("Updatedland", updated.Country);

            _dalEF.Delete(created.ManufacturerID);
        }

        [Test]
        public void Delete_ShouldRemoveManufacturer()
        {
            var newManufacturer = new DTO.Manufacture
            {
                Name = "To Be Deleted",
                Country = "Nowhere"
            };
            var created = _dalEF.Create(newManufacturer);

            var deleted = _dalEF.Delete(created.ManufacturerID);
            Assert.IsTrue(deleted);

            var fetched = _dalEF.GetById(created.ManufacturerID);
            Assert.IsNull(fetched); // Перевіряємо, що записи більше немає
        }

        [Test]
        public void GetById_NonExistingId_ShouldReturnNull()
        {
            int nonExistingId = 999999; 
            var result = _dalEF.GetById(nonExistingId);
            Assert.IsNull(result);
        }

        [Test]
        public void Delete_NonExistingId_ShouldReturnFalse()
        {
            int nonExistingId = 999999;
            var result = _dalEF.Delete(nonExistingId);
            Assert.IsFalse(result);
        }

        [Test]
        public void Update_NonExistingManufacturer_ShouldReturnFalse()
        {
            var fakeManufacturer = new DTO.Manufacture
            {
                ManufacturerID = 999999,
                Name = "Fake",
                Country = "Nowhere"
            };

            var result = _dalEF.Update(fakeManufacturer);
            Assert.IsFalse(result);
        }

    }
}
