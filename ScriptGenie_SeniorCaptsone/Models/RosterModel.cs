namespace ScriptGenie_SeniorCaptsone.Models
{
    public class RosterModel
    {
        // Declaring the model's properties
        public int RosterID { get; set; }
        public string CoachName { get; private set; }
        public LinkedList<PlayerModel> Roster { get; set; }

        /// <summary>
        /// Default constructor that initalizes class variables to null
        /// </summary>
        public RosterModel() 
        {
            RosterID = 0;
            CoachName = string.Empty;
            Roster = new LinkedList<PlayerModel>();
        }

        /// <summary>
        /// Parameterized constructor that intializes class variables to parameters
        /// </summary>
        /// <param name="rosterID"></param>
        /// <param name="coachName"></param>
        /// <param name="roster"></param>
        public RosterModel(int rosterID, string coachName, LinkedList<PlayerModel> roster)
        {
            RosterID = rosterID;
            CoachName = coachName;
            Roster = roster;
        }
    }
}
