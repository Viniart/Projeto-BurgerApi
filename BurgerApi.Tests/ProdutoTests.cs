using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using BurgerApi.Models;

namespace BurgerApi.Tests
{
    public class ProdutoTests
    {
        [Fact]
        public void AplicarDesconto_DeveReduzirPrecoCorretamente()
        {
            // Arrange (Preparar)
            var produto = new Produto { Nome = "X-Bacon", Preco = 100m };

            // Act (Agir)
            produto.AplicarDesconto(10m);

            // Assert (Verificar)
            Assert.Equal(90m, produto.Preco);
        }

        // Positivo
        // [Fact]
        // public void TemEstoque_DeveRetornarVerdadeiro_QuandoMaiorQueZero() 
        // {
        //     // AAA
        //     var produto = new Produto { QuantidadeEmEstoque = 5 };

        //     var resultado = produto.TemEstoque();

        //     Assert.True(resultado);
        // }

        // [Fact]
        // public void TemEstoque_DeveRetornarFalso_QuandoEstoqueZero() 
        // {
        //     // AAA
        //     var produto = new Produto { QuantidadeEmEstoque = 0 };

        //     var resultado = produto.TemEstoque();

        //     Assert.False(resultado);
        // }

        [Fact]
        public void Produto_ValidarNomeEInstancia()
        {
            var produto = new Produto { Nome = "X-Bacon Duplo" };

            // Testando Criacao do Produto
            Assert.NotNull(produto);

            // Nome Esta Sendo Colocado Corretamente
            Assert.Contains("Bacon", produto.Nome);
        }

        [Fact]
        public void AplicarDesconto_DeveLancarExcecao_QuandoMaiorQueCem()
        {
            var produto = new Produto {Nome = "X-Bacon", Preco = 100m};

            Assert.Throws<ArgumentException>(() => produto.AplicarDesconto(150m));
        }

        [Fact]
        public void AplicarDesconto_DeveLancarExcecaoMensagem_QuandoMaiorQueCem()
        {
            var produto = new Produto {Nome = "X-Bacon", Preco = 100m};

            var excecao = Assert.Throws<ArgumentException>(() => produto.AplicarDesconto(150m));

            Assert.Equal("O desconto deve estar entre 0 e 100", excecao.Message);
        }

    }
}