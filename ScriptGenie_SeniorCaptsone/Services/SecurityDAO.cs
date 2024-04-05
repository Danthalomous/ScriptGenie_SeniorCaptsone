using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Npgsql;
using ScriptGenie_SeniorCaptsone.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ScriptGenie_SeniorCaptsone.Services
{
    public class SecurityDAO : ISecurityService
    {
        // Connection string to the database
        string connectionString = "Host=localhost;Port=5432;Database=script_genie;Username=postgres;Password=D@myD()ggy01";

        private string Key = Environment.GetEnvironmentVariable("SECRET_KEY");

        /// <summary>
        /// Encrypts the password using AES encryption
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string EncryptPassword(string password)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.IV = new byte[16]; // Initialization vector

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                byte[] encryptedBytes = null;
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(password);
                        }
                        encryptedBytes = msEncrypt.ToArray();
                    }
                }
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        /// <summary>
        /// Decrypts an ecrypted password
        /// </summary>
        /// <param name="encryptedPassword"></param>
        /// <returns></returns>
        public string DecryptPassword(string encryptedPassword)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.IV = new byte[16]; // Initialization vector

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                byte[] cipherText = Convert.FromBase64String(encryptedPassword);
                string plaintext = null;
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
                return plaintext;
            }
        }

        /// <summary>
        /// Checks to see if the provided user is a valid and existing user.
        /// </summary>
        /// <param name="user">Model that represents the user trying to login</param>
        /// <returns></returns>
        public bool ProcessLogin(UserModel user)
        {
            string query = "SELECT * FROM users WHERE user_email = @email;";

            // Setting up the connection to the database
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    // Adding the parameters to the query
                    command.Parameters.AddWithValue("@email", user.Email);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedEncryptedPassword = reader.GetString(reader.GetOrdinal("user_password"));
                            string decryptedPassword = DecryptPassword(storedEncryptedPassword);

                            if (decryptedPassword == user.Password)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
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

            // Encrypt password
            string encryptedPassword = EncryptPassword(user.Password);

            // Insert statement for SQL
            string query = "INSERT INTO users (users_id, user_email, user_password) VALUES (@id, @email, @password)";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open(); // Opening a connection

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    // Add the values to the parameters of the query
                    command.Parameters.AddWithValue("@id", Guid.NewGuid());
                    command.Parameters.AddWithValue("@email", user.Email);
                    command.Parameters.AddWithValue("@password", encryptedPassword);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0; // Returns true if there were any rows inserted
                }
            }
        }

        /// <summary>
        /// Returns the password of the email if they are a valid user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string ProcessForgotPassword(UserModel user)
        {
            bool isValidEmail = IsEmailRegistered(user.Email);

            if (isValidEmail)
            {
                string query = "SELECT user_password FROM users where user_email = @email";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        // Adding the parameters to the query
                        command.Parameters.AddWithValue("@email", user.Email);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedEncryptedPassword = reader.GetString(reader.GetOrdinal("user_password")); // Get password
                                string decryptedPassword = DecryptPassword(storedEncryptedPassword); // Decrypt password
                                return decryptedPassword; // Return the password
                            }
                        }
                    }
                }
            }

            return "";
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

                    long count = (long)command.ExecuteScalar();

                    return count > 0; // Returns true if the email is already registered
                }
            }
        }
    }
}
