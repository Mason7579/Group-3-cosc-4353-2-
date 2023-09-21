using Microsoft.AspNetCore.Mvc;

namespace cosc_4353_project.Controllers
{
    public class ClientProfileController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }

    }
}