using cosc_4353_project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Net;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Xml;

namespace cosc_4353_project.Controllers
{
    public class FuelQuoteController : Controller
    {
        private static string _connectionString = @"Server=cosc4353-group-3.postgres.database.azure.com;Port=5432;Database=cosc4353-homework;User Id=vscode@cosc4353-group-3;Password=vscode123;";
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

        [HttpGet]
        public IActionResult FuelQuoteForm()
        {
            FuelQuoteModel profile = new FuelQuoteModel()
            {
                Client = new List<ClientProfileModel>()
            };
            ClientProfileModel clientFQ = new ClientProfileModel()
            {
                Address1 = "456 HelloWorld St. TX",
            };
            profile.Client.Add(clientFQ);
            return View(profile);
        }

        [HttpPost]
        public IActionResult FuelQuoteFormDB(double gallonsRequested, string deliveryAddress, string deliveryDate, double suggestedPrice, double totalAmountDue)
        {
            bool Submitted = false;
            string Error_Message = "";
            string SQL_Query = $"INSERT INTO \"fuel_quote\"(\"gallons_requested\",\"delivery_address\",\"delivery_date\",\"suggested_price\",\"total\",\"username_fq\",\"ID\") VALUES (@gallonsRequested,@deliveryAddress,@deliveryDate,@suggestedPrice,@totalAmountDue,@username,@id);";
            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("", conn);
                NpgsqlTransaction sqlTransaction;
                sqlTransaction = conn.BeginTransaction();
                command.Transaction = sqlTransaction;
                try
                {
                    command.CommandText = SQL_Query.ToString();
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@gallonsRequested", gallonsRequested);
                    command.Parameters.AddWithValue("@deliveryAddress", deliveryAddress);
                    command.Parameters.AddWithValue("@deliveryDate", deliveryDate);
                    command.Parameters.AddWithValue("@suggestedPrice", suggestedPrice);
                    command.Parameters.AddWithValue("@totalAmountDue", totalAmountDue);
                    command.Parameters.AddWithValue("@username", "admin");
                    command.Parameters.AddWithValue("@id", 1);
                    command.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    Submitted = true;
                }
                catch (Exception e)
                {
                    sqlTransaction.Rollback();
                    Error_Message = "There is an error while adding to database";
                }
                finally
                {
                    conn.Close();
                }
            };
            return RedirectToAction("history", "FuelQuote");
        }

    }
}
