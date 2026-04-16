using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerApi.Models;
using BurgerApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BurgerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private CategoriaRepository _repository;

        public CategoriaController(CategoriaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var categorias = await _repository.ListarCategorias();

            return Ok(categorias);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Categoria categoria)
        {
            await _repository.CadastrarCategoria(categoria);

            return Ok(new {mensagem = "Categoria cadastrada com sucesso!"});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            await _repository.RemoverCategoria(id);

            return NoContent();
        }
    }
}