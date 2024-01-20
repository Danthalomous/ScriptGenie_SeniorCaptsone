using Microsoft.Data.SqlClient;
using ScriptGenie_SeniorCaptsone.Models;

namespace ScriptGenie_SeniorCaptsone.Services
{
    public class ProfileDAO
    {
        // Connection string to the database
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ScriptGenieUserDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public bool CreateOrganization(int userID, OrganizationModel organizationModel)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Define the SQL query to insert a new organization
                    string sqlQuery = "INSERT INTO Organizations (users_id, venue_name, facility_name, organization_name, team_name, conference_relevance, competition_level) VALUES (@userId, @venueName, @facilityName, @organizationName, @teamName, @conferenceRelevance, @competitionLevel);";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        // Add parameters to the query
                        command.Parameters.AddWithValue("@userId", userID);
                        command.Parameters.AddWithValue("@venueName", organizationModel.VenueName);
                        command.Parameters.AddWithValue("@facilityName", organizationModel.FacilityName);
                        command.Parameters.AddWithValue("@organizationName", organizationModel.OrganizationName);
                        command.Parameters.AddWithValue("@teamName", organizationModel.TeamName);
                        command.Parameters.AddWithValue("@conferenceRelevance", organizationModel.ConferenceRelevance);
                        command.Parameters.AddWithValue("@competitionLevel", organizationModel.CompetitionLevel);

                        // Execute the query
                        command.ExecuteNonQuery();

                        // No need to check the number of rows affected for an INSERT operation

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                Console.WriteLine($"Error creating organization: {ex.Message}");
                return false;
            }
        }


        public LinkedList<OrganizationModel> DeleteOrganization(int userID, int organizationID)
        {
            throw new NotImplementedException();
        }

        public LinkedList<OrganizationModel> FetchAllOrganizations(int id)
        {
            throw new NotImplementedException();
        }

        public LinkedList<OrganizationModel> Update(int userID, OrganizationModel organizationModel)
        {
            throw new NotImplementedException();
        }
    }
}

