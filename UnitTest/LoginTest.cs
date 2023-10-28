using Xunit;
using Moq;
using cosc_4353_project.Controllers;
using cosc_4353_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace UnitTest
{
    public class LoginControllerTests
    {
        [Fact]
        public void Test_Login_ValidCredentials()
        {
            var mockLoginViewModel = new LoginViewModel
            {
                Username = "testuser",
                Password = "testpass"
            };

            var mockHttpContext = new Mock<HttpContext>();
            var response = new Mock<HttpResponse>();
            mockHttpContext.Setup(m => m.Response).Returns(response.Object);

            var mockCookies = new Mock<IResponseCookies>();
            response.SetupGet(r => r.Cookies).Returns(mockCookies.Object);

            var mockController = new LoginController()
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockHttpContext.Object
                }
            };

            var result = mockController.login(mockLoginViewModel);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("history", redirectResult.ActionName);
            Assert.Equal("Fuelquote", redirectResult.ControllerName);
        }

        [Fact]
        public void Test_Register_Successful()
        {

            var mockRegisterViewModel = new RegisterViewModel
            {
                Username = "testuser",
                Password = "testpass",
                ConfirmPassword = "testpass"
            };

            var mockController = new LoginController();
            var resultView = new ViewResult();

            var result = mockController.register(mockRegisterViewModel);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
        }
    }
}
