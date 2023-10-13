using cosc_4353_project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace cosc_4353_project.Models
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}