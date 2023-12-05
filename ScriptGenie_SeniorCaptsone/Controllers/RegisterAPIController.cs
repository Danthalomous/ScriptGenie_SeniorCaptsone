using Microsoft.AspNetCore.Mvc;
using ScriptGenie_SeniorCaptsone.Models;
using ScriptGenie_SeniorCaptsone.Services;

namespace ScriptGenie_SeniorCaptsone.Controllers
{
    [ApiController]
    [Route("register")]
    public class RegisterAPIController : ControllerBase
    {
        SecurityDAO securityService = new SecurityDAO(); // DAO Service to retrieve data

        /// <summary>
        /// API Get request that returns true or false if the registration was a success
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet("ProcessRegister")]
        public ActionResult<bool> ProcessRegister(UserModel user)
        {
            return securityService.ProcessRegister(user);
        }
    }
}
