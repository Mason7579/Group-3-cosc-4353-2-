using cosc_4353_project.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;
using System.Reflection.Emit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace cosc_4353_project.Controllers
{
    public class ClientProfileController : Controller
    {
        private readonly string _connectionString = @"Server=cosc4353-group-3.postgres.database.azure.com;Port=5432;Database=cosc4353-homework;User Id=vscode@cosc4353-group-3;Password=vscode123;";

        [HttpGet]
        public IActionResult Profile()
        {
            var cookie = Request.Cookies["username_cookie"];
            if (cookie == null)
            {
                return RedirectToAction("login", "Login");
            }
            DataTable datatable = new DataTable();
            bool Submitted = false;
            string Error_Message = "";
            string SQL_Query = $"SELECT * FROM \"profile\" WHERE \"username_pf\"=@username ";
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
                ClientProfileModel clientNE = new ClientProfileModel()
                {
                    Address1 = "",
                    City = "",
                    State = "",
                    Zipcode = "",
                    FullName = "",
                    Address2 = "",

                };
                return View(clientNE);
            }
            ClientProfileModel client = new ClientProfileModel()
            {
                Address1 = datatable.Rows[0]["address_1"].ToString(),
                City = datatable.Rows[0]["city"].ToString(),
                State = datatable.Rows[0]["state"].ToString(),
                Zipcode = datatable.Rows[0]["zipcode"].ToString(),
                FullName = datatable.Rows[0]["full_name"].ToString(),
                Address2 = datatable.Rows[0]["address_2"].ToString(),

            };
            return View(client);
        }

        [HttpPost]
        public IActionResult SaveProfile(ClientProfileModel model)
        {
            var username = Request.Cookies["username_cookie"];
                using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();

                    if (ProfileExists())
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand("UPDATE profile SET full_name = @FullName, address_1 = @Address1, address_2 = @Address2, city = @City, state = @State, zipcode = @Zipcode WHERE username_pf = @Username", conn))
                        {
                            command.Parameters.AddWithValue("@FullName", model.FullName);
                            command.Parameters.AddWithValue("@Address1", model.Address1);

                            if (model.Address2 != null)
                            {
                                command.Parameters.AddWithValue("@Address2", NpgsqlDbType.Varchar, model.Address2);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Address2", DBNull.Value);
                            }

                            command.Parameters.AddWithValue("@City", model.City);
                            command.Parameters.AddWithValue("@State", model.State);
                            command.Parameters.AddWithValue("@Zipcode", model.Zipcode);
                            command.Parameters.AddWithValue("@Username", username);

                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO profile (full_name, address_1, address_2, city, state, zipcode, username_pf) VALUES (@FullName, @Address1, @Address2, @City, @State, @Zipcode, @Username)", conn))
                        {
                            command.Parameters.AddWithValue("@FullName", model.FullName);
                            command.Parameters.AddWithValue("@Address1", model.Address1);

                            if (model.Address2 != null)
                            {
                                command.Parameters.AddWithValue("@Address2", NpgsqlDbType.Varchar, model.Address2);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Address2", DBNull.Value);
                            }

                            command.Parameters.AddWithValue("@City", model.City);
                            command.Parameters.AddWithValue("@State", model.State);
                            command.Parameters.AddWithValue("@Zipcode", model.Zipcode);
                            command.Parameters.AddWithValue("@Username", username);

                            command.ExecuteNonQuery();
                        }
                    }

                    model.SuccessMessage = "Profile saved successfully!";

                    return View("Profile", model);
                }
        }

        private bool ProfileExists()
        {
            var cookie = Request.Cookies["username_cookie"];
            DataTable datatable = new DataTable();
            bool Submitted = false;
            string Error_Message = "";
            string SQL_Query = $"SELECT * FROM \"profile\" WHERE \"username_pf\"=@username ";
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
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
