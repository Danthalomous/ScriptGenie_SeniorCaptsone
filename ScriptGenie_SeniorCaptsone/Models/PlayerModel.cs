namespace ScriptGenie_SeniorCaptsone.Models
{
    public class PlayerModel
    {
        // Declaration of model's properties
        public Guid PlayerID { get; set; }
        public string PlayerName { get; set; }
        public string PlayerPosition { get; set; }
        public int PlayerNumber { get; set; }
        public bool IsStarting { get; set; }

        /// <summary>
        /// Default constructor that intializes class variables to null
        /// </summary>
        public PlayerModel()
        {
            PlayerID = Guid.NewGuid();
            PlayerName = string.Empty;
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
        public PlayerModel(Guid playerID, string playerNamee, string playerPosition, int playerNumber, bool isStarting)
        {
            PlayerID = playerID;
            PlayerName = playerNamee;
            PlayerPosition = playerPosition;
            PlayerNumber = playerNumber;
            IsStarting = isStarting;
        }
    }
}
