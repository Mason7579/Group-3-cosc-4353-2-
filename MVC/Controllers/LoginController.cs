using Microsoft.AspNetCore.Mvc;

namespace cosc_4353_project.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult login()
        {
            return View();
        }
        public IActionResult register()
        {
            return View();
        }
    }
}
