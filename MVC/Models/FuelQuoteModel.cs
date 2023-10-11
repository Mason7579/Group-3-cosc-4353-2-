using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using cosc_4353_project.Models;



namespace cosc_4353_project.Models
{
    public class FuelQuoteModel
    {
        public double? Gallons_Requested { get; set; }
        public string? Client_UserName { get; set; }
        public string? Delivery_Date { get; set; }
        public double? Suggested_Price { get; set; }
        public double? Total_Amount_Due { get; set; }
        public string? Delivery_Address { get; set; }

        public List<ClientProfileModel> Client = new List<ClientProfileModel>();
    }

    public class FuelQuoteHistory
    {
        public List<FuelQuoteModel> History = new List<FuelQuoteModel>();
    }
}
