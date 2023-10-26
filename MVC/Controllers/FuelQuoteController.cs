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
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Emit;

namespace cosc_4353_project.Controllers
{
    public class FuelQuoteController : Controller
    {
        private static string _connectionString = @"Server=cosc4353-group-3.postgres.database.azure.com;Port=5432;Database=cosc4353-homework;User Id=vscode@cosc4353-group-3;Password=vscode123;";
        [HttpGet]
        public IActionResult history()
        {
            var cookie = Request.Cookies["username_cookie"];
            DataTable datatable = new DataTable();
            bool Submitted = false;
            string Error_Message = "";
            string SQL_Query = $"SELECT \"gallons_requested\", \"delivery_address\", \"delivery_date\", \"suggested_price\", \"total\" FROM \"fuel_quote\" WHERE \"username_fq\"=@username ";
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
                    command.Parameters.AddWithValue("@username", cookie);
                    NpgsqlDataAdapter sqlDataAdapter = new NpgsqlDataAdapter(command);
                    sqlDataAdapter.Fill(datatable);
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
            if (datatable.Rows.Count == 0)
            {
                return RedirectToAction("FuelQuoteForm", "FuelQuote");
            }
            FuelQuoteHistory view = new FuelQuoteHistory()
            {
                History = new List<FuelQuoteModel>()
            };
            foreach (DataRow row in datatable.Rows)
            {
                FuelQuoteModel FuelQuote = new FuelQuoteModel()
                {
                    Gallons_Requested = Convert.ToSingle(row["gallons_requested"]),
                    Delivery_Date = row["delivery_date"].ToString(),
                    Suggested_Price = Convert.ToSingle(row["suggested_price"]),
                    Total_Amount_Due = Convert.ToSingle(row["total"]),
                    Delivery_Address = row["delivery_address"].ToString(),
                };
                view.History.Add(FuelQuote);
            }
            if (cookie != null)
            {
                return View(view);
            }
            else
            {
                return RedirectToAction("login", "Login");
            } 
        }

        [HttpGet]
        public IActionResult FuelQuoteForm()
        {
            var cookie = Request.Cookies["username_cookie"];
            DataTable datatable = new DataTable();
            bool Submitted = false;
            string Error_Message = "";
            string SQL_Query = $"SELECT \"address_1\", \"city\", \"state\", \"zipcode\" FROM \"profile\" WHERE \"username_pf\"=@username ";
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
                    command.Parameters.AddWithValue("@username", cookie);
                    NpgsqlDataAdapter sqlDataAdapter = new NpgsqlDataAdapter(command);
                    sqlDataAdapter.Fill(datatable);
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
            if (datatable.Rows.Count == 0)
            {
                return RedirectToAction("login", "Login");
            }
            FuelQuoteModel profile = new FuelQuoteModel()
            {
                Client = new List<ClientProfileModel>()
            };
            ClientProfileModel clientFQ = new ClientProfileModel()
            {
                Address1 = datatable.Rows[0]["address_1"].ToString(),
                City = datatable.Rows[0]["city"].ToString(),
                State = datatable.Rows[0]["state"].ToString(),
                Zipcode = datatable.Rows[0]["zipcode"].ToString(),
            };
            profile.Client.Add(clientFQ);
            if (cookie != null)
            {
                return View(profile);
            }
            else
            {
                return RedirectToAction("login", "Login");
            }
        }

        [HttpPost]
        public IActionResult FuelQuoteFormDB(double gallonsRequested, string deliveryAddress, string deliveryDate, double suggestedPrice, double totalAmountDue)
        {
            var cookie = Request.Cookies["username_cookie"];
            bool Submitted = false;
            string Error_Message = "";
            string SQL_Query = $"INSERT INTO \"fuel_quote\"(\"gallons_requested\",\"delivery_address\",\"delivery_date\",\"suggested_price\",\"total\",\"username_fq\") VALUES (@gallonsRequested,@deliveryAddress,@deliveryDate,@suggestedPrice,@totalAmountDue,@username);";
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
                    command.Parameters.AddWithValue("@username", cookie);
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
