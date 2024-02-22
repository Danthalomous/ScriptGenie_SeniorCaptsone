using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Npgsql;
using ScriptGenie_SeniorCaptsone.Models;

namespace ScriptGenie_SeniorCaptsone.Services
{
    public class SecurityDAO : ISecurityService
    {
        // Connection string to the database
        string connectionString = "Host=localhost;Port=5432;Database=script_genie;Username=postgres;Password=D@myD()ggy01";

        /// <summary>
        /// Checks to see if the provided user is a valid and existing user.
        /// </summary>
        /// <param name="user">Model that represents the user trying to login</param>
        /// <returns></returns>
        public bool ProcessLogin(UserModel user)
        {
            string query = "SELECT * FROM users WHERE user_email = @email AND user_password = @password;";

            // Setting up the connection to the database
            using(NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using(NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    // Adding the parameters to the query
                    command.Parameters.AddWithValue("@email", user.Email);
                    command.Parameters.AddWithValue("@password", user.Password);

                    object result = command.ExecuteScalar(); // Executing the command

                    if (result != null && result != DBNull.Value) // If the command result was not null
                    {
                        return true;
                    }
                    else
                    {
                        // Handle the case when there are no matching rows
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the userID of the desired user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Guid GetUserID(string email)
        {
            string query = "SELECT users_id FROM users WHERE user_email = @email";

            // Setting up the connection to the database
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    // Adding the parameters to the query
                    command.Parameters.AddWithValue("@email", email);

                    object result = command.ExecuteScalar(); // Executing the command

                    if (result != null && result != DBNull.Value) // If the command result was not null
                    {
                        return (Guid)result;
                    }
                    else
                    {
                        // Handle the case when there are no matching rows
                        return Guid.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// Inserts the user into the database
        /// </summary>
        /// <param name="user">Model that represents the user trying to register</param>
        /// <returns></returns>
        public bool ProcessRegister(UserModel user)
        {
            // Check if the email is already registered
            if (IsEmailRegistered(user.Email))
            {
                return false; // Email is already registered, registration failed
            }

            // Insert statement for SQL
            string query = "INSERT INTO users (user_email, user_password) VALUES (@email, @password)";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open(); // Opening a connection

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    // Add the values to the parameters of the query
                    command.Parameters.AddWithValue("@email", user.Email);
                    command.Parameters.AddWithValue("@password", user.Password);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0; // Returns true if there were any rows inserted
                }
            }
        }

        public bool ProcessForgotPassword(UserModel user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if the email is already registered in the database
        /// </summary>
        /// <param name="email">Email to check</param>
        /// <returns>True if the email is already registered, false otherwise</returns>
        private bool IsEmailRegistered(string email)
        {
            string query = "SELECT COUNT(*) FROM users WHERE user_email = @email";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);

                    int count = (int)command.ExecuteScalar();

                    return count > 0; // Returns true if the email is already registered
                }
            }
        }
    }
}
