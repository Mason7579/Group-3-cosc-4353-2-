using Xunit;
using cosc_4353_project.Controllers;
using cosc_4353_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace UnitTest
{
    public class ClientProfileTest
    {
        [Fact]
        public void Test_SaveProfile_Post_Valid_Model()
        {
            var controller = new ClientProfileController();
            var model = new ClientProfileModel
            {
                FullName = "John Doe",
                Address1 = "123 Main St",
                City = "Some City",
                State = "TX",
                Zipcode = "12345"
            };
            var result = controller.SaveProfile(model) as RedirectToActionResult;
            Assert.NotNull(result);
            Assert.Equal("ProfileSaved", result.ActionName);
        }

        [Fact]
        public void Test_SaveProfile_Post_Invalid_Model()
        {
            var controller = new ClientProfileController();
            var model = new ClientProfileModel
            {
                FullName = null,
                Address1 = "123 Main St",
                City = "Some City",
                State = "TX",
                Zipcode = "12345"
            };
            controller.ModelState.AddModelError("FullName", "Full Name is required");
            var result = controller.SaveProfile(model) as ViewResult;
            Assert.NotNull(result);
            Assert.Equal("Profile", result.ViewName);
        }
    }
}
