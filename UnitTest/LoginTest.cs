using Xunit;
using cosc_4353_project.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTest
{
    public class LoginTest
    {
        [Fact]
        public void Test_Login_return_view()
        {
            var controller = new LoginController();
            var result = controller.login() as ViewResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void Test_Login_with_credentials_return_view()
        {
            var username = "testusername";
            var password = "testpassword";
            var controller = new LoginController();
            var result = controller.login(username, password) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal("Login successful!", result.ViewData["Message"]);
        }

    }
}