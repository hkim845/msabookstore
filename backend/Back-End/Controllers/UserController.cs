using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Back_End.Models;
using Back_End.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Back_End.Controllers
{
    [Route("/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            var newItem = await _service.CreateUser(user);
            return CreatedAtAction(nameof(CreateUser), new { id = newItem.Id }, newItem);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> CreateToken(User user)
        {
            user = await _service.Authenticate(user.Username, user.Password);            

            if (user == null)
                return Unauthorized();

            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var issuer = "https://localhost:7206/";
            var audience = "http://localhost:3000/";
            var key = Encoding.ASCII.GetBytes("my-32-character-ultra-secure-and-ultra-long-secret");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
            }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return Ok(new { token = jwtToken });
        }
    }
}
