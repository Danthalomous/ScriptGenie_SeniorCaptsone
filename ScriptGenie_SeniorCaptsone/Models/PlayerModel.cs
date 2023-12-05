namespace ScriptGenie_SeniorCaptsone.Models
{
    public class PlayerModel
    {
        // Declaration of model's properties
        public int PlayerID { get; private set; }
        public string PlayerNamee { get; private set; }
        public string PlayerPosition { get; private set; }
        public int PlayerNumber { get; private set; }
        public bool IsStarting { get; private set; }

        /// <summary>
        /// Default constructor that intializes class variables to null
        /// </summary>
        public PlayerModel()
        {
            PlayerID = 0;
            PlayerNamee = string.Empty;
            PlayerPosition = string.Empty;
            PlayerNumber = 0;
            IsStarting = false;
        }

        /// <summary>
        /// Parameterized constructor that intializes class variables to parameters
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="playerNamee"></param>
        /// <param name="playerPosition"></param>
        /// <param name="playerNumber"></param>
        /// <param name="isStarting"></param>
        public PlayerModel(int playerID, string playerNamee, string playerPosition, int playerNumber, bool isStarting)
        {
            PlayerID = playerID;
            PlayerNamee = playerNamee;
            PlayerPosition = playerPosition;
            PlayerNumber = playerNumber;
            IsStarting = isStarting;
        }
    }
}
