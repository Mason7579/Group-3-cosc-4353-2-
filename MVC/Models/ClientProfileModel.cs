using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using cosc_4353_project.Models;



namespace cosc_4353_project.Models
{
    public class ClientProfileModel
    {
        [Required(ErrorMessage = "Full Name is required.")]
        [MaxLength(50, ErrorMessage = "Full Name cannot exceed 50 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Address 1 is required.")]
        [MaxLength(100, ErrorMessage = "Address 1 cannot exceed 100 characters.")]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Zipcode is required.")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Please enter a valid zipcode.")]
        public string Zipcode { get; set; }
    }
}
