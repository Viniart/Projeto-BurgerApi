using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BurgerApi.DTOs;
using BurgerApi.Models;
using BurgerApi.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BurgerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private UsuarioRepository _repository;
        private IConfiguration _config;

        public LoginController(UsuarioRepository repository, 
        IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        // 1. DTO Login
        // 2. Busca por Email
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            // 1. Pesquisar por Email
            var usuario = await _repository.BuscarPorEmail(dto.Email);
            if (usuario == null) 
                return Unauthorized("Usuario ou senha invalidos.");

            // 2. Verificar se a Senha bate
            var hasher = new PasswordHasher<Usuario>();
            var resultado = hasher.VerifyHashedPassword(usuario, 
                usuario.Senha, dto.Senha);

            if (resultado != PasswordVerificationResult.Success)
                return Unauthorized("Usuario ou senha invalidos.");

            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var creds = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            );

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, usuario.Perfil)
            };

            var token = new JwtSecurityToken(
                issuer: "HburguersAPI",
                audience: "HburguersApp",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new {token = tokenString});
        }
    }
}