using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TradingCompany.BL.Interfaces;
using TradingCompany.DTO;
using TradingCompany.WebApp.Controllers;
using Xunit;

namespace TradingCompany.WebApp.Tests
{
    public class AccountControllerTests
    {
        private readonly Mock<IAuthManager> _mockAuthManager;
        private readonly Mock<HttpContext> _mockHttpContext;
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            _mockAuthManager = new Mock<IAuthManager>();
            _controller = new AccountController(_mockAuthManager.Object);
        }

        [Fact]
        public void Login_Get_ReturnsView()
        {
            // Act
            var result = _controller.Login();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Login_Post_InvalidCredentials_ReturnsViewWithError()
        {
            // Arrange
            _mockAuthManager.Setup(a => a.Login("user", "wrongpass")).Returns(false);

            // Act
            var result = await _controller.Login("user", "wrongpass");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.False(_controller.ModelState.IsValid == true); // ModelState має помилку
        }

        [Fact]
        
        public async Task Login_Post_ValidCredentials_RedirectsToReturnUrl()
        {
            // Arrange
            var mockAuthManager = new Mock<IAuthManager>();
            var user = new User { Login = "user", Privileges = new List<Privilege>() };
            mockAuthManager.Setup(a => a.Login("user", "pass")).Returns(true);
            mockAuthManager.Setup(a => a.GetUserByLogin("user")).Returns(user);

            var controller = new AccountController(mockAuthManager.Object);

            // Mock IAuthenticationService
            var mockAuthService = new Mock<IAuthenticationService>();
            mockAuthService.Setup(a => a.SignInAsync(
                It.IsAny<HttpContext>(),
                It.IsAny<string>(),
                It.IsAny<ClaimsPrincipal>(),
                It.IsAny<AuthenticationProperties>()
            )).Returns(Task.CompletedTask);

            // Setup HttpContext with RequestServices
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(sp => sp.GetService(typeof(IAuthenticationService))).Returns(mockAuthService.Object);

            var httpContext = new DefaultHttpContext
            {
                RequestServices = serviceProvider.Object
            };

            controller.ControllerContext.HttpContext = httpContext;

            // Act
            var result = await controller.Login("user", "pass", "/Home/Index");

            // Assert
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/Home/Index", redirectResult.Url);
        }

        [Fact]
        public async Task Logout_RedirectsToLogin()
        {
            // Arrange
            var mockHttpContext = new Mock<Microsoft.AspNetCore.Http.HttpContext>();
            mockHttpContext.Setup(h => h.SignOutAsync(It.IsAny<string>())).Returns(Task.CompletedTask);
            _controller.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await _controller.Logout();

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirectResult.ActionName);
        }

        [Fact]
        public void AccessDenied_ReturnsView()
        {
            // Act
            var result = _controller.AccessDenied();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }
    }
}
