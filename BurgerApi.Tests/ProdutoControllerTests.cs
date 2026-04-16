using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerApi.Controllers;
using BurgerApi.Models;
using BurgerApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BurgerApi.Tests
{
    public class ProdutoControllerTests
    {
        [Fact]
        public async Task ListarTodos_DeveRetornarOk_ComListaDeProdutos()
        {
            var listaFalsa = new List<Produto>
            {
                new Produto {ProdutoID = 1, Nome = "X-Bacon"},
                new Produto {ProdutoID = 2, Nome = "X-Salada"}
            };

            var mockRepository = new Mock<IProdutoRepository>();

            mockRepository
                .Setup(r => r.ListarProdutos())
                .ReturnsAsync(listaFalsa);

            var controller = new ProdutoController(mockRepository.Object);

            var resultado = await controller.ListarTodos();

            Assert.IsType<OkObjectResult>(resultado);
        }
    }
}