using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ScriptGenie_SeniorCaptsone.Models;
using ScriptGenie_SeniorCaptsone.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ScriptGenie_SeniorCaptsone.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginAPIController : ControllerBase
    {
        SecurityDAO securityService = new SecurityDAO(); // DAO Service to retrieve data

        [HttpGet("GetUserID")]
        public ActionResult<Guid> GetUserID(string email)
        {
            if(email == null)
                return BadRequest("Invalid email");
            
            return securityService.GetUserID(email);
        }

        /// <summary>
        /// API Post request that returns true or false on whether or not the user has a valid login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("ProcessLogin")]
        public ActionResult<bool> ProcessLogin([FromBody] UserModel user)
        {
            // Validate user credentials
            if (securityService.ProcessLogin(user))
            {
                // If the login is valid, generate a JWT token
                var token = GenerateJwtToken(user.Email);

                // Return the token in the response
                return Ok(new { Token = token });
            }

            // If authentication fails, return unauthorized status
            return Unauthorized();
        }

        /// <summary>
        /// Method that creates a JWT token for authorization of the user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private string GenerateJwtToken(string email)
        {
            // Using built-in tools to make a JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("ASPNETCORE_JWT_SECRET_KEY"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time (can change)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
 