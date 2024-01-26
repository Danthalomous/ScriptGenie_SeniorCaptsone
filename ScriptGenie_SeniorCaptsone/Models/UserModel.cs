namespace ScriptGenie_SeniorCaptsone.Models
{
    public class UserModel
    {
        // Declaration of properties
        public Guid UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// Default constructor that intializes variables with null values
        /// </summary>
        public UserModel()
        {
            UserID = Guid.NewGuid();
            Email = string.Empty;
            Password = string.Empty;
        }

        /// <summary>
        /// Parameterized constructor intializing class variables to the parameters
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        public UserModel(Guid userID, string email, string password)
        {
            UserID = userID;
            Email = email;
            Password = password;
        }
    }
}
