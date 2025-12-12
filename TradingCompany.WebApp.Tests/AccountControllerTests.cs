using Microsoft.AspNetCore.Mvc;
using Moq;
using TradingCompany.BL.Interfaces;
using TradingCompany.DTO;
using TradingCompany.WebApp.Controllers;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TradingCompany.WebApp.Tests
{
    public class AccountControllerTests
    {
        [Fact]
        public async Task Login_ValidUser_ReturnsRedirectToReturnUrl()
        {
            // Arrange
            var mockAuthManager = new Mock<IAuthManager>();
            var testUser = new User
            {
                Login = "admin",
                Privileges = new List<Privilege> { new Privilege { Name = "Admin" } }
            };

            mockAuthManager.Setup(x => x.Login("admin", "password")).Returns(true);
            mockAuthManager.Setup(x => x.GetUserByLogin("admin")).Returns(testUser);

            var controller = new AccountController(mockAuthManager.Object);

            // Щоб SignInAsync не падало
            var httpContext = new DefaultHttpContext();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = await controller.Login("admin", "password", "/Category");

            // Assert
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/Category", redirectResult.Url);
        }
    }
}
