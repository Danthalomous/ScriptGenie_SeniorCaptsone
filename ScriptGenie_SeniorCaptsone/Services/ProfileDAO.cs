using Npgsql;
using ScriptGenie_SeniorCaptsone.Models;

namespace ScriptGenie_SeniorCaptsone.Services
{
    public class ProfileDAO
    {
        // Connection string to the database
        string connectionString = "Host=localhost;Port=5432;Database=script_genie;Username=postgres;Password=D@myD()ggy01";


        //--------------------------------------------------------------------------------------------------------------------------------
        // Organizations
        //--------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Method that creates a new organization entry into the database
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="organizationModel"></param>
        /// <returns></returns>
        public bool CreateOrganization(Guid userID, OrganizationModel organizationModel)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Define the SQL query to insert a new organization
                    string sqlQuery = "INSERT INTO Organizations (organizations_id, users_id, rosters_id, venue_name, facility_name, organization_name, team_name, conference_relevance, competition_level) VALUES (@organizations_id, @userId, @rosters_id, @venueName, @facilityName, @organizationName, @teamName, @conferenceRelevance, @competitionLevel);";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        // Add parameters to the query
                        command.Parameters.AddWithValue("@organizations_id", organizationModel.OrganizationID);
                        command.Parameters.AddWithValue("@userId", userID);
                        command.Parameters.AddWithValue("@rosters_id", organizationModel.OrganizationID);
                        command.Parameters.AddWithValue("@venueName", organizationModel.VenueName);
                        command.Parameters.AddWithValue("@facilityName", organizationModel.FacilityName);
                        command.Parameters.AddWithValue("@organizationName", organizationModel.OrganizationName);
                        command.Parameters.AddWithValue("@teamName", organizationModel.TeamName);
                        command.Parameters.AddWithValue("@conferenceRelevance", organizationModel.ConferenceRelevance);
                        command.Parameters.AddWithValue("@competitionLevel", organizationModel.CompetitionLevel);

                        // Execute the query
                        command.ExecuteNonQuery();

                        return true; // Successful insert
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

        /// <summary>
        /// Method that gets all organizations under that user's id
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public LinkedList<OrganizationModel> FetchAllOrganizations(Guid userID)
        {
            LinkedList<OrganizationModel> returnThese = new LinkedList<OrganizationModel>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Define the SQL query to fetch organizations for a specific user_id
                    string sqlQuery = "SELECT * FROM Organizations WHERE users_id = @userId;";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        // Add parameter to the query
                        command.Parameters.AddWithValue("@userId", userID);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Create OrganizationModel objects and add them to the list
                                OrganizationModel organization = new OrganizationModel
                                {
                                    OrganizationID = reader.GetGuid(reader.GetOrdinal("organizations_id")),
                                    VenueName = reader.GetString(reader.GetOrdinal("venue_name")),
                                    FacilityName = reader.GetString(reader.GetOrdinal("facility_name")),
                                    OrganizationName = reader.GetString(reader.GetOrdinal("organization_name")),
                                    TeamName = reader.GetString(reader.GetOrdinal("team_name")),
                                    ConferenceRelevance = reader.GetString(reader.GetOrdinal("conference_relevance")),
                                    CompetitionLevel = reader.GetString(reader.GetOrdinal("competition_level"))
                                    // Add other properties as needed
                                };

                                returnThese.AddLast(organization);
                            }
                        }

                        return returnThese;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                Console.WriteLine($"Error fetching organizations: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Method that deletes a specific organization
        /// </summary>
        /// <param name="organizatinonID"></param>
        /// <returns></returns>
        public bool DeleteOrganization(Guid organizatinonID)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString)) 
            {
                connection.Open();

                string sqlQuery = "DELETE FROM organizations WHERE organizations_id = @id";

                using(NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection)) 
                {
                    command.Parameters.AddWithValue("@id", organizatinonID);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if rows were affected
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting organization: {ex.Message}"); // TODO: Properly handle this
                        return false;
                    }
                }
            }

        }

        /// <summary>
        /// Method that updates an existing model to the new model
        /// </summary>
        /// <param name="organizationID"></param>
        /// <param name="organizationModel"></param>
        /// <returns></returns>
        public bool UpdateOrganization(Guid organizationID, OrganizationModel organizationModel)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "UPDATE organizations SET venue_name = @venueName, facility_name = @facilityName, organization_name = @organizationName, team_name = @teamName, conference_relevance = @conferenceRelevance, competition_level = @competitionLevel WHERE organizations_id = @organizationsId";

                using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                {
                    // Adding values to the command's parameters
                    command.Parameters.AddWithValue("@venueName", organizationModel.VenueName);
                    command.Parameters.AddWithValue("@facilityName", organizationModel.FacilityName);
                    command.Parameters.AddWithValue("@organizationName", organizationModel.OrganizationName);
                    command.Parameters.AddWithValue("@teamName", organizationModel.TeamName);
                    command.Parameters.AddWithValue("@conferenceRelevance", organizationModel.ConferenceRelevance);
                    command.Parameters.AddWithValue("@competitionLevel", organizationModel.CompetitionLevel);
                    command.Parameters.AddWithValue("@organizationsId", organizationID);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if rows were affected
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error updating organization: {ex.Message}"); // TODO: Properly handle this
                        return false;
                    }
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------
        // Rosters
        //--------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Method that creates a new roster in the database
        /// </summary>
        /// <param name="organizationID"></param>
        /// <param name="rosterModel"></param>
        /// <returns></returns>
        public bool CreateRoster(Guid organizationID, RosterModel rosterModel)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Define the SQL query to insert a new organization
                    string sqlQuery = "INSERT INTO Rosters (rosters_id, organizations_id, players_id, coach_name) VALUES (@rostersID, @organizationsID, @playersID, @coachName);";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        // Add parameters to the query
                        command.Parameters.AddWithValue("@rostersID", Guid.NewGuid());
                        command.Parameters.AddWithValue("@organizationsID", organizationID);
                        command.Parameters.AddWithValue("@playersID", rosterModel.PlayerID);
                        command.Parameters.AddWithValue("@coachName", rosterModel.CoachName);

                        // Execute the query
                        command.ExecuteNonQuery();

                        return true; // Successful insert
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

        /// <summary>
        /// Method that gets all rosters from the database under a specific organization
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public LinkedList<RosterModel> FetchAllRosters(Guid organizationID)
        {
            LinkedList<RosterModel> returnThese = new LinkedList<RosterModel>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Define the SQL query to fetch organizations for a specific user_id
                    string sqlQuery = "SELECT * FROM Rosters WHERE organizations_id = @organizationID;";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        // Add parameter to the query
                        command.Parameters.AddWithValue("@organizationID", organizationID);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Create RosterModel objects and add them to the list
                                RosterModel roster = new RosterModel
                                {
                                    RosterID = reader.GetGuid(reader.GetOrdinal("rosters_id")),
                                    CoachName = reader.GetString(reader.GetOrdinal("coach_name")),
                                    PlayerID = reader.GetGuid(reader.GetOrdinal("players_id")),
                                    Roster = new LinkedList<PlayerModel>()
                                };

                                returnThese.AddLast(roster);
                            }
                        }

                        return returnThese;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                Console.WriteLine($"Error fetching rosters: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Method that updates a roster with a new roster based on the rosterID
        /// </summary>
        /// <param name="rosterID"></param>
        /// <param name="rosterModel"></param>
        /// <returns></returns>
        public bool UpdateRoster(Guid rosterID, RosterModel rosterModel)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "UPDATE rosters SET coach_name = @coachName WHERE rosters_id = @rostersID";

                using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                {
                    // Adding values to the command's parameters
                    command.Parameters.AddWithValue("@coachName", rosterModel.CoachName);
                    command.Parameters.AddWithValue("@rostersID", rosterID);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if rows were affected
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error updating roster: {ex.Message}"); // TODO: Properly handle this
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Method that deletes a roster according to a specific ID
        /// </summary>
        /// <param name="rosterID"></param>
        /// <returns></returns>
        public bool DeleteRoster(Guid rosterID)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "DELETE FROM rosters WHERE rosters_id = @rostersID";

                using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@rostersID", rosterID);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if rows were affected
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting roster: {ex.Message}"); // TODO: Properly handle this
                        return false;
                    }
                }
            }

        }
    }
}

