using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using ScriptGenie_SeniorCaptsone.Models;
using ScriptGenie_SeniorCaptsone.Services;

namespace ScriptGenie_SeniorCaptsone.Controllers
{
    [ApiController]
    [Route("profile")]
    public class ProfileAPIController : ControllerBase
    {
        private ProfileDAO profileService = new ProfileDAO(); // Instance of the DAO

        // Local class that provides a different way to get information from the requester
        public class ProfileRequest<T>
        {
            public T Model { get; set; }
            public Guid Id { get; set; }
        }

        /// <summary>
        /// Method that calls the DAO and attempts to create a new organization
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create/organization")]
        public IActionResult CreateOrganization([FromBody] ProfileRequest<OrganizationModel> request)
        {
            if (request == null)
                return BadRequest("Invalid request sent");
            try
            {
                // Check if the organization can be created
                if (profileService.CreateOrganization(request.Id, request.Model))
                {
                    return Ok("Organization Created Successfully"); // Success!
                }
                else
                {
                    return BadRequest("Invalid Object Submitted"); // Failure!
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error"); // Something is wrong in the DAO
            }
        }

        /// <summary>
        /// Method that calls the DAO and attempts to get all organizations
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet("fetchAll/organizations")]
        public IActionResult FetchAllOrganizations([FromQuery] Guid userID)
        {
            // Check to see if null (it really can't be but good habits)
            if (userID == null)
                return BadRequest("Invalid id");

            try
            {
                LinkedList<OrganizationModel> organizationList = profileService.FetchAllOrganizations(userID); // list to return

                if (organizationList.Count > 0)
                {
                    // Successful fetch, return the list
                    return Ok(organizationList);
                }
                else
                {
                    // No organizations found for the given userID
                    return NotFound("No organizations found for the specified user.");
                }
            }
            catch (Exception ex)
            {
                // TODO: Handle exceptions (e.g., log the error)
                Console.WriteLine($"Error fetching organizations: {ex.Message}");

                // Return a 500 Internal Server Error
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Method that calls the DAO and attempts to delete an organization
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpDelete("delete/organization")]
        public IActionResult DeleteOrganization([FromQuery] Guid organizationID)
        {
            // Ensure the id is not null (it really can't be but good habits)
            if (organizationID == null)
                return BadRequest("Invalid body");

            try
            {
                // Check if the organization can be deleted
                if (profileService.DeleteOrganization(organizationID)) return Ok("Organization was succesfully deleted"); // Success!
                else return BadRequest("Error when trying to delete organization"); // Failure!
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error"); // Something is wrong in the DAO
            }
        }

        /// <summary>
        /// Method that calls the DAO and attempts to update an organization
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("update/organization")]
        public IActionResult UpdateOrganization([FromBody] ProfileRequest<OrganizationModel> request)
        {
            // Check to make sure it's not null
            if (request == null)
                return BadRequest("Invalid object submitted");

            try
            {
                // Check if the model can be updated
                if (profileService.UpdateOrganization(request.Id, request.Model)) return Ok("Succesfully updated organization!"); // Success!
                else return BadRequest("Error when updating model"); // Failure!
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error"); // Something in the DAO has a problem
            }
        }

        /// <summary>
        /// Method that calls the DAO and attempts to create a roster
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create/roster")]
        public IActionResult CreateRoster([FromBody] ProfileRequest<RosterModel> request)
        {
            // Checking if null
            if (request == null)
                return BadRequest("Invalid request sent");
            try
            {
                // Check if the organization can be created
                if (profileService.CreateRoster(request.Id, request.Model))
                {
                    return Ok("Roster Created Successfully"); // Success!
                }
                else
                {
                    return BadRequest("Invalid Object Submitted"); // Failure!
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error"); // Something is wrong in the DAO
            }
        }

        /// <summary>
        /// Method that calls the DAO and attempts to to get all rosters
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        [HttpGet("fetchAll/rosters")]
        public IActionResult FetchAllRosters([FromQuery] Guid organizationID)
        {
            // Check to see if null (it really can't be but good habits)
            if (organizationID == null)
                return BadRequest("Invalid id");

            try
            {
                LinkedList<RosterModel> rosterList = profileService.FetchAllRosters(organizationID); // list to return

                if (rosterList.Count > 0)
                {
                    // Successful fetch, return the list
                    return Ok(rosterList); // Success!
                }
                else
                {
                    // No organizations found for the given userID
                    return NotFound("No organizations found for the specified user."); // Failure!
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal Server Error"); // Something is wrong in the DAO
            }
         }

        /// <summary>
        /// Method that calls the DAO and attempts to update a specific roster
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("update/roster")]
        public IActionResult UpdateRosters([FromBody] ProfileRequest<RosterModel> request)
        {
            // Check to make sure it's not null
            if (request == null)
                return BadRequest("Invalid object submitted");

            try
            {
                // Check if the model can be updated
                if (profileService.UpdateRoster(request.Id, request.Model)) return Ok("Succesfully updated roster!"); // Success!
                else return BadRequest("Error when updating model"); // Failure!
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error"); // Something in the DAO has a problem
            }
        }

        /// <summary>
        /// Method that calls the DAO and attempts to delete a specific roster
        /// </summary>
        /// <param name="rosterID"></param>
        /// <returns></returns>
        [HttpDelete("delete/roster")]
        public IActionResult DeleteRoster([FromQuery] Guid rosterID)
        {
            // Ensure the id is not null (it really can't be but good habits)
            if (rosterID == null)
                return BadRequest("Invalid body");

            try
            {
                // Check if the organization can be deleted
                if (profileService.DeleteRoster(rosterID)) return Ok("Roster was succesfully deleted"); // Success!
                else return BadRequest("Error when trying to delete roster"); // Failure!
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error"); // Something is wrong in the DAO
            }
        }
    }
}
