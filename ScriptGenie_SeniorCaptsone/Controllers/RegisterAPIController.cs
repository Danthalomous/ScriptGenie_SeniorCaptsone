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
    [Route("register")]
    public class RegisterAPIController : ControllerBase
    {
        SecurityDAO securityService = new SecurityDAO(); // DAO Service to retrieve data

        /// <summary>
        /// API Post request that returns true or false if the registration was a success
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("ProcessRegister")]
        public ActionResult<string> ProcessRegister([FromBody] UserModel user)
        {
            if (securityService.ProcessRegister(user))
            {
                // Registration successful, generate and return JWT token
                string token = GenerateJwtToken(user.Email);
                return Ok(token);
            }

            // Registration failed
            return BadRequest("Registration failed");
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

