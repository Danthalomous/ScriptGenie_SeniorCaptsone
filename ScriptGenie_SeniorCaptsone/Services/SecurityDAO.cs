using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using ScriptGenie_SeniorCaptsone.Models;

namespace ScriptGenie_SeniorCaptsone.Services
{
    public class SecurityDAO : ISecurityService
    {
        // Connection string to the database
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ScriptGenieUserDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        /// <summary>
        /// Checks to see if the provided user is a valid and existing user.
        /// </summary>
        /// <param name="user">Model that represents the user trying to login</param>
        /// <returns></returns>
        public bool ProcessLogin(UserModel user)
        {
            string query = "SELECT * FROM users WHERE user_email = @email AND user_password = @password;";

            // Setting up the connection to the database
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    // Adding the parameters to the query
                    command.Parameters.AddWithValue("@email", user.Email);
                    command.Parameters.AddWithValue("@password", user.Password);

                    object result = command.ExecuteScalar(); // Executing the command

                    if (result != null && result != DBNull.Value) // If the command result was not null
                    {
                        long count = (long)result;
                        return count > 0; // will return true if there WERE any rows
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
        /// Inserts the user into the database
        /// </summary>
        /// <param name="user">Model that represents the user trying to register</param>
        /// <returns></returns>
        public bool ProcessRegister(UserModel user)
        {
            // Insert statment for SQL
            string query = "INSERT INTO users (user_email, user_password) VALUES (@email, @password)";

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // Opening a connection

                using (SqlCommand command = new SqlCommand(query, connection))
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
    }
}
