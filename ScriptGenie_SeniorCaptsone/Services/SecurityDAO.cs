using Microsoft.Data.SqlClient;
using ScriptGenie_SeniorCaptsone.Models;

namespace ScriptGenie_SeniorCaptsone.Services
{
    public class SecurityDAO : ISecurityService
    {
        // Connection string to the database
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ScriptGenieUserDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Application Intent=ReadWrite;Multi Subnet Failover=False";


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

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
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

        public bool ProcessRegister(UserModel user)
        {
            throw new NotImplementedException();
        }

        public bool ProcessForgotPassword(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
