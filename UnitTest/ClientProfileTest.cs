using System;
using System.Data;
using cosc_4353_project.Controllers;
using cosc_4353_project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public interface IYourRepository
{

    ClientProfileModel GetProfileByUsername(string username);

}

namespace UnitTest
{
    public class ClientProfileControllerTest
    {
        [Fact]
        public void Test_Profile_ReturnsViewWhenCookieIsNotNull()
        {
            var controller = new ClientProfileController();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Cookie"] = "username_cookie=admin";
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = controller.Profile() as ViewResult;

            Assert.NotNull(result);
            Assert.IsType<ClientProfileModel>(result.Model);
        }

        [Fact]
        public void Test_Profile_RedirectsToLoginWhenCookieIsNull()
        {
            var controller = new ClientProfileController();
            var httpContext = new DefaultHttpContext();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = controller.Profile() as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("login", result.ActionName);
            Assert.Equal("Login", result.ControllerName);
        }

        [Fact]
        public void Test_Profile_ReturnsViewWhenDatabaseQueryReturnsNoRows()
        {
            
        }

        [Fact]
        public void Test_Profile_ReturnsViewWhenExceptionDuringDatabaseInteraction()
        {

        }

        [Fact]
        public void Test_Profile_ReturnsViewWhenDatabaseQueryReturnsRows()
        {

        }

        [Fact]
        public void Test_SaveProfile_UpdatesProfileWhenProfileExists()
        {

            var controller = new ClientProfileController();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Cookie"] = "username_cookie=admin";
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var mockRepository = new Mock<IYourRepository>();

            mockRepository.Setup(repo => repo.GetProfileByUsername(It.IsAny<string>()))
                          .Returns(new ClientProfileModel { Address1 = "123 Main St", /* Set up other profile properties */ });

            var model = new ClientProfileModel
            {
                FullName = "Germa66", 
                Address1 = "123 Main St",
                City = "Houston",
                State = "TX",
                Zipcode = "77056"
            };
            var result = controller.SaveProfile(model) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal("Profile", result.ViewName);
            Assert.IsType<ClientProfileModel>(result.Model);
            Assert.Equal("Profile saved successfully!", ((ClientProfileModel)result.Model).SuccessMessage);
        }

        [Fact]
        public void Test_SaveProfile_InsertsNewProfileWhenProfileDoesNotExist()
        {
            
        }

        [Fact]
        public void Test_SaveProfile_ReturnsViewWhenExceptionDuringDatabaseInteraction()
        {
           
        }
    }
}
