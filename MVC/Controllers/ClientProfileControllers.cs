using cosc_4353_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace cosc_4353_project.Controllers
{
    public class ClientProfileController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Profile(ClientProfileModel model)
        {
            if (ModelState.IsValid)
            {
                return Content("Profile saved successfully!");
            }
            return View("Profile", model);
        }

    }
}
