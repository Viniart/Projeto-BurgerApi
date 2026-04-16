using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerApi.Models;

namespace BurgerApi.Repositories
{
    public interface IProdutoRepository
    {
        public Task AdicionarProduto(Produto produto);
        public Task<List<Produto>> ListarProdutos();
        public Task<Produto> BuscarPorId(int id);
        public Task AtualizarPreco(int id, decimal novoPreco);
        public Task AtualizarStatus(int id, bool novoStatus);
        public Task AtualizarProduto(Produto produtoAtualizado);
        public Task RemoverProduto(int id);
        public Task<bool> VerificarSeNomeExiste(string nome);
        public Task<int> ContarProdutos();
        public Task<List<Produto>> ObterLanchesPremium();
    }
}