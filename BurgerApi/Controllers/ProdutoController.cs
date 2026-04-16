using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerApi.DTOs;
using BurgerApi.Models;
using BurgerApi.Repositories;
using BurgerApi.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace BurgerApi.Controllers
{
    // ApiController - Informa ao .NET que é um controller
    [ApiController]
    // Route - Rota do nosso controller
    [Route("api/[controller]")]
    // [Authorize]
    public class ProdutoController : ControllerBase
    {
        // Injeção de Dependência - Controller -> Repository
        private IProdutoRepository _repository;

        public ProdutoController(IProdutoRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Cadastra um novo produto.
        /// </summary>
        /// <param name="dto">Objeto DTO contendo as informações (nome, preco, etc...)</param>
        /// <remarks>
        ///     Exemplo de Requisição:
        ///     
        ///     POST /api/produto
        ///         {
        ///             "nome": "X-Bacon",
        ///             "descricao": "Lanche X"
        ///             "preco": 20m
        ///             "CategoriaID": 1
        ///         }
        /// </remarks>
        /// <response code="201">Retorna o lanche criado</response>
        /// <response code="400">Caso os dados enviados sejam inválidos</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Cadastrar(ProdutoRequisicaoDTO dto)
        {
            // Validacao
            var validador = new ProdutoValidator();

            var resultado = validador.Validate(dto);

            // Se NAO for valido
            if(!resultado.IsValid)
            {
                return BadRequest(resultado.Errors);
            }


            var produto = new Produto
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Preco = dto.Preco,
                CategoriaID = dto.CategoriaId,
                Status = true,
                Imagem = ""
            };

            await _repository.AdicionarProduto(produto);

            return CreatedAtAction(nameof(BuscarPorId), new
            {
                id = produto.ProdutoID
            }, produto);
        }

        // Metodo (GET, POST, PUT/PATCH, DELETE)
        /// <summary>
        /// Método para listar todos os Produtos.
        /// </summary>
        /// <returns>Uma lista de produtos e status code 200 OK</returns>
        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var produtos = await _repository.ListarProdutos();

            // Converto de Produto para DTO
            var produtosDTO = produtos.Select(
                p => new ProdutoRespostaDTO
                {
                    Id = p.ProdutoID,
                    Nome = p.Nome,
                    Preco = p.Preco,
                    Status = p.Status
                }
            ).ToList();

            return Ok(produtosDTO);
        }
        
        // Listar por Id
        
        

        /// <summary>
        /// Busca um produto especifico por Id
        /// </summary>
        /// <param name="id">Id do Produto</param>
        /// <response code="200">Retorna o Produto solicitado</response>
        /// <response code="404">Caso o Produto nao exista no Banco de Dados</response>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var produto = await _repository.BuscarPorId(id);

            if (produto == null)
            {
                return NotFound("Lanche nao encontrado no cardapio!");
            }

            return Ok(produto);
        }

        [HttpPatch("{id}/preco")]
        public async Task<IActionResult> AtualizarPreco(int id,
            [FromBody] decimal novoPreco)
        {
            await _repository.AtualizarPreco(id, novoPreco);

            return Ok(new {mensagem = "Preco atualizado com sucesso!"});
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> AtualizarStatus(int id,
            [FromBody] bool novoStatus)
        {
            await _repository.AtualizarStatus(id, novoStatus);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(Produto produtoAtualizado)
        {
            await _repository.AtualizarProduto(produtoAtualizado);

            // 200 - Ok
            // 204 - No Content
            return NoContent();
        }
        
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoverProduto(int id)
        {
            await _repository.RemoverProduto(id);

            // return Ok(new {mensagem = "Produto Excluido!"});
            return NoContent();
        }

        [HttpGet("premium")]
        public async Task<IActionResult> ListarPremium()
        {
             var produtos = await _repository.ObterLanchesPremium();

            return Ok(produtos);
        }

        // Informar os detalhes de um produto
        [HttpGet("detalhes/{id}")]
        public async Task<IActionResult> InformarDetalhes(int id)
        {
            return Ok(new { mensagem = "Buscando detalhes do produto... " });
        }

        // Pesquisar por categoria ou por tamanho
        [HttpGet("pesquisar")]
        public async Task<IActionResult> Pesquisar([FromQuery] string categoria, [FromQuery] string tamanho)
        {
            return Ok(new { mensagem = "Pesquisando... " });
        }
        
    }
}