using Microsoft.AspNetCore.Mvc;

namespace cosc_4353_project.Controllers
{
    public class FuelQuoteController : Controller
    {
        public IActionResult history()
        {
            return View();
        }
    }
}
