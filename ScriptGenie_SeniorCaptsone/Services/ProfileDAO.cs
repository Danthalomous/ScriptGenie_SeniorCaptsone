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
                                    OrganizationID = reader.IsDBNull(reader.GetOrdinal("organizations_id")) ? Guid.Empty : reader.GetGuid(reader.GetOrdinal("organizations_id")),
                                    VenueName = reader.IsDBNull(reader.GetOrdinal("venue_name")) ? null : reader.GetString(reader.GetOrdinal("venue_name")),
                                    FacilityName = reader.IsDBNull(reader.GetOrdinal("facility_name")) ? null : reader.GetString(reader.GetOrdinal("facility_name")),
                                    OrganizationName = reader.IsDBNull(reader.GetOrdinal("organization_name")) ? null : reader.GetString(reader.GetOrdinal("organization_name")),
                                    TeamName = reader.IsDBNull(reader.GetOrdinal("team_name")) ? null : reader.GetString(reader.GetOrdinal("team_name")),
                                    ConferenceRelevance = reader.IsDBNull(reader.GetOrdinal("conference_relevance")) ? null : reader.GetString(reader.GetOrdinal("conference_relevance")),
                                    CompetitionLevel = reader.IsDBNull(reader.GetOrdinal("competition_level")) ? null : reader.GetString(reader.GetOrdinal("competition_level"))
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
                    string sqlQuery = "INSERT INTO Rosters (rosters_id, organizations_id, players_id, coach_name, roster_name) VALUES (@rostersID, @organizationsID, @playersID, @coachName, @rosterName);";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        // Add parameters to the query
                        command.Parameters.AddWithValue("@rostersID", rosterModel.RosterID);
                        command.Parameters.AddWithValue("@organizationsID", organizationID);
                        command.Parameters.AddWithValue("@playersID", rosterModel.PlayerID);
                        command.Parameters.AddWithValue("@coachName", rosterModel.CoachName);
                        command.Parameters.AddWithValue("@rostername", rosterModel.RosterName);

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
                                    RosterName = reader.GetString(reader.GetOrdinal("roster_name")),
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

                string sqlQuery = "UPDATE rosters SET coach_name = @coachName, roster_name = @rosterName WHERE rosters_id = @rostersID";

                using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                {
                    // Adding values to the command's parameters
                    command.Parameters.AddWithValue("@coachName", rosterModel.CoachName);
                    command.Parameters.AddWithValue("@rosterName", rosterModel.RosterName);
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

        //----------------------------------------------------------------------------------------------------------------------------
        // Players
        //----------------------------------------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Method that creates a new player in the database
        /// </summary>
        /// <param name="rosterID"></param>
        /// <param name="playerModel"></param>
        /// <returns></returns>
        public bool CreatePlayer(Guid rosterID, PlayerModel playerModel)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Define the SQL query to insert a new organization
                    string sqlQuery = "INSERT INTO Players (players_id, rosters_id, player_name, player_position, player_number, player_starting) VALUES (@playersID, @rostersID, @playerName, @playerPosition, @playerNumber, @playerStarting);";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        // Add parameters to the query
                        command.Parameters.AddWithValue("@playersID", playerModel.PlayerID);
                        command.Parameters.AddWithValue("@rostersID", rosterID);
                        command.Parameters.AddWithValue("@playerName", playerModel.PlayerName);
                        command.Parameters.AddWithValue("@playerPosition", playerModel.PlayerPosition);
                        command.Parameters.AddWithValue("@playerNumber", playerModel.PlayerNumber);
                        command.Parameters.AddWithValue("@playerStarting", playerModel.IsStarting);

                        // Execute the query
                        command.ExecuteNonQuery();

                        return true; // Successful insert
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                Console.WriteLine($"Error creating player: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Method that gets all players from the database under a specific roster
        /// </summary>
        /// <param name="rosterID"></param>
        /// <returns></returns>
        public LinkedList<PlayerModel> FetchAllPlayers(Guid rosterID)
        {
            LinkedList<PlayerModel> returnThese = new LinkedList<PlayerModel>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Define the SQL query to fetch organizations for a specific user_id
                    string sqlQuery = "SELECT * FROM Players WHERE rosters_id = @rosterID;";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        // Add parameter to the query
                        command.Parameters.AddWithValue("@rosterID", rosterID);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Create RosterModel objects and add them to the list
                                PlayerModel player = new PlayerModel
                                {
                                    PlayerID = reader.GetGuid(reader.GetOrdinal("players_id")),
                                    PlayerName = reader.GetString(reader.GetOrdinal("player_name")),
                                    PlayerPosition = reader.GetString(reader.GetOrdinal("player_position")),
                                    PlayerNumber = reader.GetInt32(reader.GetOrdinal("player_number")),
                                    IsStarting = reader.GetBoolean(reader.GetOrdinal("player_starting"))
                                };

                                returnThese.AddLast(player);
                            }
                        }

                        return returnThese;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                Console.WriteLine($"Error fetching players: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Method that updates a player with a new player based on the playerID
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="playerModel"></param>
        /// <returns></returns>
        public bool UpdatePlayer(Guid playerID, PlayerModel playerModel)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "UPDATE players SET player_name = @playerName, player_position = @playerPosition, player_number = @playerNumber, player_starting = @isStarting WHERE players_ID = @playerID";

                using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                {
                    // Adding values to the command's parameters
                    command.Parameters.AddWithValue("@playerName", playerModel.PlayerName);
                    command.Parameters.AddWithValue("@playerPosition", playerModel.PlayerPosition);
                    command.Parameters.AddWithValue("@playerNumber", playerModel.PlayerNumber);
                    command.Parameters.AddWithValue("@isStarting", playerModel.IsStarting);
                    command.Parameters.AddWithValue("@playerID", playerID);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if rows were affected
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error updating player: {ex.Message}"); // TODO: Properly handle this
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Method that deletes a player according to a specific ID
        /// </summary>
        /// <param name="playerID"></param>
        /// <returns></returns>
        public bool DeletePlayer(Guid playerID)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "DELETE FROM players WHERE players_id = @playerID";

                using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@playerID", playerID);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if rows were affected
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting player: {ex.Message}"); // TODO: Properly handle this
                        return false;
                    }
                }
            }
        }
    }
}

