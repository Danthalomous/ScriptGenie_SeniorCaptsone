namespace ScriptGenie_SeniorCaptsone.Models
{
    public class OrganizationModel
    {
        // Declaring the model's properties
        public Guid OrganizationID { get; set; }
        public LinkedList<RosterModel> Rosters { get; set; }
        public string VenueName { get; set; }
        public string FacilityName { get; set; }
        public string OrganizationName { get; set; }
        public string TeamName { get; set; }
        public string ConferenceRelevance { get; set; }
        public string CompetitionLevel { get; set; }
        
        /// <summary>
        /// Default constructor that initalizes the class variables to null values
        /// </summary>
        public OrganizationModel() 
        {
            OrganizationID = Guid.NewGuid();
            Rosters = new LinkedList<RosterModel>();
            VenueName = string.Empty;
            FacilityName = string.Empty;
            OrganizationName = string.Empty;
            TeamName = string.Empty;
            ConferenceRelevance = string.Empty;
            CompetitionLevel = string.Empty;
        }

        /// <summary>
        /// Parameterized constructor that intializes class variables to parameters
        /// </summary>
        /// <param name="organizationID"></param>
        /// <param name="rosters"></param>
        /// <param name="venueName"></param>
        /// <param name="facilityName"></param>
        /// <param name="organizationName"></param>
        /// <param name="teamName"></param>
        /// <param name="conferenceRelevance"></param>
        /// <param name="competitionLevel"></param>
        public OrganizationModel(Guid organizationID, LinkedList<RosterModel> rosters, string venueName, string facilityName, string organizationName, string teamName, string conferenceRelevance, string competitionLevel)
        {
            OrganizationID = organizationID;
            Rosters = rosters;
            VenueName = venueName;
            FacilityName = facilityName;
            OrganizationName = organizationName;
            TeamName = teamName;
            ConferenceRelevance = conferenceRelevance;
            CompetitionLevel = competitionLevel;
        }
    }
}
