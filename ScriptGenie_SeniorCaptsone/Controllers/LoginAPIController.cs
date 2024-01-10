using Microsoft.AspNetCore.Mvc;
using ScriptGenie_SeniorCaptsone.Models;
using ScriptGenie_SeniorCaptsone.Services;

namespace ScriptGenie_SeniorCaptsone.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginAPIController : ControllerBase
    {
        SecurityDAO securityService = new SecurityDAO(); // DAO Service to retrieve data

        /// <summary>
        /// API Post request that returns true or false on whether or not the user has a valid login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("ProcessLogin")]
        public ActionResult<bool> ProcessLogin([FromBody] UserModel user)
        {
            return securityService.ProcessLogin(user);
        }
    }
}
 