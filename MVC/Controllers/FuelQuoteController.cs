using cosc_4353_project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace cosc_4353_project.Controllers
{
    public class FuelQuoteController : Controller
    {
        [HttpGet]
        public IActionResult history()
        {

            FuelQuoteHistory view = new FuelQuoteHistory()
            {
                History = new List<FuelQuoteModel>()
            };

            FuelQuoteModel FuelQuote = new FuelQuoteModel()
            {
                Gallons_Requested = 139,
                Delivery_Date = "12/08/2022",
                Suggested_Price = 2.90,
                Total_Amount_Due = 403.1,
                Delivery_Address = "123 HelloWorld St. TX",
            };
            view.History.Add(FuelQuote);

            return View(view);
        }
        public IActionResult FuelQuoteForm()
        {
            return View();
        }

    }
}
