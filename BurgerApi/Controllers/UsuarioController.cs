using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerApi.DTOs;
using BurgerApi.Models;
using BurgerApi.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BurgerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private UsuarioRepository _repository;

        public UsuarioController(UsuarioRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(UsuarioRequisicaoDTO dto)
        {
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Perfil = dto.Perfil
            };

            var hasher = new PasswordHasher<Usuario>();
            usuario.Senha = hasher.HashPassword(usuario, dto.Senha);

            await _repository.Cadastrar(usuario);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var usuarios = await _repository.ListarTodos();

            var usuariosDTO = usuarios.Select(
                u => new UsuarioRespostaDTO
                {
                    Id = u.UsuarioId,
                    Nome = u.Nome,
                    Email = u.Email,
                    Perfil = u.Perfil
                }
            ).ToList();

            return Ok(usuariosDTO);
        }
    }
}