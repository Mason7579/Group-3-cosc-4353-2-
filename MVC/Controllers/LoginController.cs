using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace cosc_4353_project.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult login(string username, string password)
        {
            string Username = username;
            string Password = password;
            // TODO: Add validation logic here

            // For now, just returning to the login view with a success message
            Response.Cookies.Append("username_cookies", Username);
            ViewBag.Message = "Login successful!";
            return View();
        }

        [HttpGet]
        public IActionResult register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult register(string username, string password, string confirmPassword)
        {
            // TODO: Add validation logic here

            // For now, just returning to the register view with a success message
            ViewBag.Message = "Registration successful!";
            return View();
        }
    }
}
