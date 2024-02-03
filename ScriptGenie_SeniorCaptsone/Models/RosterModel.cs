namespace ScriptGenie_SeniorCaptsone.Models
{
    public class RosterModel
    {
        // Declaring the model's properties
        public Guid RosterID { get; set; }
        public string CoachName { get; set; }
        public Guid PlayerID { get; set; }
        public LinkedList<PlayerModel> Roster { get; set; }

        /// <summary>
        /// Default constructor that initalizes class variables to null
        /// </summary>
        public RosterModel() 
        {
            RosterID = Guid.NewGuid();
            CoachName = string.Empty;
            PlayerID = Guid.NewGuid();
            Roster = new LinkedList<PlayerModel>();
        }

        /// <summary>
        /// Parameterized constructor that intializes class variables to parameters
        /// </summary>
        /// <param name="rosterID"></param>
        /// <param name="coachName"></param>
        /// <param name="roster"></param>
        public RosterModel(Guid rosterID, string coachName, LinkedList<PlayerModel> roster, Guid playerID)
        {
            RosterID = rosterID;
            CoachName = coachName;
            Roster = roster;
            PlayerID = playerID;
        }
    }
}
