using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductAndRequests.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ProductAndRequests.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("generate-token")]
        public IActionResult GenerateToken([FromBody] TokenRequest request)
        {
            // Validate the user credentials here. For demo, we'll skip validation.
            // In a real application, you should authenticate the user with your user store.

            var jwtKey = _configuration["Jwt:Key"];

            if(jwtKey == null)
            {
                return BadRequest();
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(120), // Token validity period
                signingCredentials: credentials
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
    }
}