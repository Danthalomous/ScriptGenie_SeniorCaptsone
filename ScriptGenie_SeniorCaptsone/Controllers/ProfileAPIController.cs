using Microsoft.AspNetCore.Mvc;
using ScriptGenie_SeniorCaptsone.Models;
using ScriptGenie_SeniorCaptsone.Services;

namespace ScriptGenie_SeniorCaptsone.Controllers
{
    [ApiController]
    [Route("profile")]
    public class ProfileAPIController<T> : ControllerBase
    {
        // TODO: NEEDS WORK
        private ProfileDAO<T> profileService;

        [HttpPost("Create")]
        public ActionResult<LinkedList<T>> Create([FromBody] T model, [FromBody] int userID)
        {
            return profileService.Create(userID, model);
        }
    }
}
