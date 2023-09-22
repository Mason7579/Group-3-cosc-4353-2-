using Microsoft.AspNetCore.Mvc;

namespace cosc_4353_project.Controllers
{
    public class FuelQuoteFormControllers : Controller
    {
        public IActionResult Form()
        {
            return View();
        }
    }
}