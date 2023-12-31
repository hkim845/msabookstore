﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Back_End.Models;
using Back_End.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IConfiguration Configuration;

        public UserController(IUserService service, IConfiguration configuration)
        {
            _service = service;
            Configuration = configuration;
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

            var issuer = Configuration["Jwt:Issuer"];
            var audience = Configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);

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
