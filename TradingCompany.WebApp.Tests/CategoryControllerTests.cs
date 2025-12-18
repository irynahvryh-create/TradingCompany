using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.BL.Interfaces;
using TradingCompany.DTO;
using TradingCompany.WebApp.Controllers;
using TradingCompany.WebApp.Models;

namespace TradingCompany.WebApp.Tests
{
    public class CategoryControllerTests
    {
        private readonly Mock<ICategoryManager> _mockManager;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<CategoryController>> _mockLogger;
        private readonly CategoryController _controller;

        public CategoryControllerTests()
        {
            _mockManager = new Mock<ICategoryManager>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<CategoryController>>();

            _controller = new CategoryController(_mockManager.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public void Index_ReturnsViewWithCategories()
        {
            // Arrange
            var categories = new List<Category> { new Category { CategoryID = 1, Name = "Test", Status = true } };
            _mockManager.Setup(m => m.GetAllCategories()).Returns(categories);

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(categories, viewResult.Model);
        }

        [Fact]
        public void Create_Get_ReturnsView()
        {
            // Act
            var result = _controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<EditCategoryModel>(viewResult.Model);
        }

        [Fact]
        public void Create_Post_ValidModel_CreatesCategoryAndRedirects()
        {
            // Arrange
            var model = new EditCategoryModel { CategoryID = 1, Name = "Test", Status = true };
            var dto = new Category { CategoryID = 1, Name = "Test", Status = true };

            _mockMapper.Setup(m => m.Map<Category>(model)).Returns(dto);
            _mockManager.Setup(m => m.CreateCategory(dto));

            // Act
            var result = _controller.Create(model);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            _mockManager.Verify(m => m.CreateCategory(dto), Times.Once);
        }

        [Fact]
        public void Edit_Get_ExistingId_ReturnsViewWithModel()
        {
            // Arrange
            var dto = new Category { CategoryID = 1, Name = "Test", Status = true };
            var model = new EditCategoryModel { CategoryID = 1, Name = "Test", Status = true };

            _mockManager.Setup(m => m.GetCategoryById(1)).Returns(dto);
            _mockMapper.Setup(m => m.Map<EditCategoryModel>(dto)).Returns(model);

            // Act
            var result = _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(model, viewResult.Model);
        }

        [Fact]
        public void Edit_Post_ValidModel_UpdatesCategoryAndRedirects()
        {
            // Arrange
            var model = new EditCategoryModel { CategoryID = 1, Name = "Test", Status = true };
            var dto = new Category { CategoryID = 1, Name = "Test", Status = true };

            _mockMapper.Setup(m => m.Map<Category>(model)).Returns(dto);

            // Act
            var result = _controller.Edit(model);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            _mockManager.Verify(m => m.UpdateCategory(dto), Times.Once);
        }

        [Fact]
        public void DeleteConfirmed_ValidId_DeletesCategoryAndRedirects()
        {
            // Arrange
            _mockManager.Setup(m => m.DeleteCategory(1)).Returns(true);

            // Act
            var result = _controller.DeleteConfirmed(1);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            _mockManager.Verify(m => m.DeleteCategory(1), Times.Once);
        }
    }
}
