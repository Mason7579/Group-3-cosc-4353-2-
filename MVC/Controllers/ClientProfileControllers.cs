using cosc_4353_project.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;

namespace cosc_4353_project.Controllers
{
    public class ClientProfileController : Controller
    {
        private readonly string _connectionString = @"Server=cosc4353-group-3.postgres.database.azure.com;Port=5432;Database=cosc4353-homework;User Id=vscode@cosc4353-group-3;Password=vscode123;";

        [HttpGet]
        public IActionResult Profile()
        {
            string username = GetLoggedInUsername();
            ClientProfileModel profile = GetClientProfile(username);

            if (profile == null)
            {
                profile = new ClientProfileModel();
            }

            return View(profile);
        }

        [HttpPost]
        public IActionResult SaveProfile(ClientProfileModel model)
        {
            string username = GetLoggedInUsername();

            if (ModelState.IsValid)
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();

                    if (ProfileExists(username))
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

            return View("Profile", model);
        }

        private bool ProfileExists(string username)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(*) FROM profile WHERE username_pf = @Username", conn))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public IActionResult ProfileSaved()
        {
            return View();
        }

        private string GetLoggedInUsername()
        {
            return "admin";
        }

        private ClientProfileModel GetClientProfile(string username)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM profile WHERE username_pf = @Username", conn))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ClientProfileModel profile = new ClientProfileModel
                            {
                                FullName = reader["full_name"].ToString(),
                                Address1 = reader["address_1"].ToString(),
                                Address2 = reader["address_2"].ToString(),
                                City = reader["city"].ToString(),
                                State = reader["state"].ToString(),
                                Zipcode = reader["zipcode"].ToString(),
                                Username = reader["username_pf"].ToString()
                            };

                            return profile;
                        }
                    }
                }
            }

            return null;
        }
    }
}
