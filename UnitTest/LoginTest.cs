using cosc_4353_project.Controllers;
using cosc_4353_project.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace UnitTest
{
    public class LoginControllerTest
    {
        [Fact]
        public void Test_Login_Get_ReturnsView()
        {
            // Arrange
            var controller = new LoginController();

            // Act
            var result = controller.login() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Test_Login_Post_WithValidCredentials_RedirectsToHistory()
        {
            // Arrange
            var controller = new LoginController();
            var model = new LoginViewModel
            {
                Username = "admin",
                Password = "admin123"
            };

            // Act
            var result = controller.login(model) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("history", result.ActionName);
            Assert.Equal("Fuelquote", result.ControllerName);
        }

        [Fact]
        public void Test_Login_Post_WithInvalidCredentials_ReturnsViewWithMessage()
        {
            // Arrange
            var controller = new LoginController();
            var model = new LoginViewModel
            {
                Username = "invalidusername",
                Password = "invalidpassword"
            };

            // Act
            var result = controller.login(model) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Invalid credentials!", controller.ViewBag.Message);
        }
    }
}