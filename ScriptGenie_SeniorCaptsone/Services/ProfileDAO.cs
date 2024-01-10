using Microsoft.Data.SqlClient;
using ScriptGenie_SeniorCaptsone.Models;

namespace ScriptGenie_SeniorCaptsone.Services
{
    public class ProfileDAO<T> : ICRUDDataService<T>
    {
        // Connection string to the database
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ScriptGenieUserDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public LinkedList<T> Create(int userID, T newModel)
        {
            // Based on what type of object that was recieved, the appropriate private create function is called
            if (newModel is OrganizationModel)
            {
                if(CreateOrganization(userID, newModel as OrganizationModel))
                {
                    return FetchAll(userID);
                }
                else
                {
                    return null; // TODO: FIX
                }
            }
            /*else if (newModel is PlayerModel)
            {
                //CreatePlayer(userID, newModel);
                return FetchAll(userID);
            }
            else if (newModel is RosterModel)
            {
                //CreateRoster(userID, newModel);
                return FetchAll(userID);
            }*/
            else
            {
                return null;
                // TODO: Error handling
            }
        }

        public LinkedList<T> Delete(int userID, int organizationID)
        {
            throw new NotImplementedException();
        }

        public LinkedList<T> FetchAll(int userID)
        {
            throw new NotImplementedException();
        }

        public LinkedList<T> Update(int userID, T newModel)
        {
            throw new NotImplementedException();
        }

        private bool CreateOrganization(int userID, OrganizationModel newOrganization)
        {
            // Insert statment for SQL
            string query = "INSERT INTO organizations (users_id, rosters_id, venue_name, facility_name, organization_name, team_name, conference_relevance, competition_level) VALUES (@usersID, SCOPE_IDENTITY(), @venueName, @facilityName, @organizationName, @teamName, @conferenceRelevance, @competitionLevel)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // Opening a connection

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add the values to the parameters of the query
                    command.Parameters.AddWithValue("@usersID", userID);
                    command.Parameters.AddWithValue("@venueName", newOrganization.VenueName);
                    command.Parameters.AddWithValue("@facilityName", newOrganization.FacilityName);
                    command.Parameters.AddWithValue("@organizationName", newOrganization.OrganizationName);
                    command.Parameters.AddWithValue("@teamName", newOrganization.TeamName);
                    command.Parameters.AddWithValue("@conferenceRelevance", newOrganization.ConferenceRelevance);
                    command.Parameters.AddWithValue("@competitionLevel", newOrganization.CompetitionLevel);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0; // Returns true if there were any rows inserted
                }
            }
        }
    }
}
