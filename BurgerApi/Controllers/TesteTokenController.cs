using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BurgerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TesteTokenController : ControllerBase
    {
        [HttpGet]
        public IActionResult GerarTokenTeste()
        {
            var key = Encoding.UTF8.GetBytes("MinhaChaveSecretaDaHamburgueria123456!");
            var creds = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: "HburguersAPI",
                audience: "HburguersApp",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new {token = tokenString});
        }
    }
}