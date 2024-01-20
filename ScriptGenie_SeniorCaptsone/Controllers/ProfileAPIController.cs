using Microsoft.AspNetCore.Mvc;
using ScriptGenie_SeniorCaptsone.Models;
using ScriptGenie_SeniorCaptsone.Services;

namespace ScriptGenie_SeniorCaptsone.Controllers
{
    [ApiController]
    [Route("profile")]
    public class ProfileAPIController : ControllerBase
    {
        private ProfileDAO profileService = new ProfileDAO();

        public class CreateProfileRequest<T>
        {
            public T Model { get; set; }
            public int Id { get; set; }
        }

        [HttpPost("create/organization")]
        public IActionResult CreateOrganization([FromBody] CreateProfileRequest<OrganizationModel> request)
        {
            if(profileService.CreateOrganization(request.Id, request.Model))
            {
                return Ok("Organization Created Successfully");
            }
            else
            {
                return BadRequest("Invalid Object Submitted");
            }
        }
    }
}
