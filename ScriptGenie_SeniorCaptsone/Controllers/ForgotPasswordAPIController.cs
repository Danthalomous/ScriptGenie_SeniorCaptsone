using Microsoft.AspNetCore.Mvc;
using ScriptGenie_SeniorCaptsone.Models;
using ScriptGenie_SeniorCaptsone.Services;

namespace ScriptGenie_SeniorCaptsone.Controllers
{
    [ApiController]
    [Route("ForgotPassword")]
    public class ForgotPasswordAPIController : ControllerBase
    {
        SecurityDAO securityService = new SecurityDAO(); // DAO Service to retrieve data


        /// <summary>
        /// API Get request that verifies that the user can recieve a forgot password and returns true or false on that request.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("ProcessForgotPassword")]
        public ActionResult<string> ProcessForgotPassword(UserModel user)
        {
            return securityService.ProcessForgotPassword(user);
        }
    }
}
