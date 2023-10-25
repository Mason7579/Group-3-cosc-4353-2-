using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Security.Cryptography;
using System.Text;
using cosc_4353_project.Models;

namespace cosc_4353_project.Controllers
{
    public class LoginController : Controller
    {
        // It's safer to not hard-code the connection string directly in the controller.
        // Consider using a configuration manager or a secrets manager.
        private readonly string connectionString = @"Server=cosc4353-group-3.postgres.database.azure.com;Port=5432;Database=cosc4353-homework;User Id=vscode@cosc4353-group-3;Password=vscode123;"; 

        [HttpGet]
        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult login(LoginViewModel model)
        {
            var hashedPassword = ComputeSha256Hash(model.Password);
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM login WHERE username_lg = @username AND password = @hashedPassword", connection))
                {
                    cmd.Parameters.AddWithValue("username", model.Username);
                    cmd.Parameters.AddWithValue("hashedPassword", hashedPassword);

                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        Response.Cookies.Append("username_cookie", model.Username);
                        
                        
                        return RedirectToAction("Profile", "ClientProfile");

                    }
                    else
                    {
                        ViewBag.Message = "Invalid credentials!";
                        return View();
                    }
                }
            }
        }

        [HttpGet]
        public IActionResult register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult register(RegisterViewModel model)
        {
            Console.WriteLine("Registering user: " + model.Username);
            Console.WriteLine("Registering password: " + model.Password);
            Console.WriteLine("Registering confirm password: " + model.ConfirmPassword);
            if (model.Password != model.ConfirmPassword)
            {
                ViewBag.Message = "Passwords do not match!";
                return View();
            }

            var hashedPassword = ComputeSha256Hash(model.Password);
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO login (username_lg, password) VALUES (@username, @hashedPassword)", connection))
                {
                    cmd.Parameters.AddWithValue("username", model.Username);
                    cmd.Parameters.AddWithValue("hashedPassword", hashedPassword);
                    cmd.ExecuteNonQuery();
                }
            }

            ViewBag.Message = "Registration successful!";
            return View();
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
