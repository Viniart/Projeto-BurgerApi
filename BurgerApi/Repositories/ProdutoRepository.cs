using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerApi.Data;
using BurgerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerApi.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        // Assincrono
        public async Task AdicionarProduto(Produto produto)
        {
            _context.Produtos.Add(produto);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Produto>> ListarProdutos()
        {
            return await _context.Produtos
                // OPCIONAL
                // As no Tracking - Nao guarda os produtos na memoria
                .AsNoTracking()
                .Include(p => p.Categoria)
                .ToListAsync();
        }

        public async Task<Produto> BuscarPorId(int id)
        {
            return await _context.Produtos
                .FirstOrDefaultAsync(p => p.ProdutoID == id);
        }

        // Edite o Preco do Produto
        public async Task AtualizarPreco(int id, decimal novoPreco)
        {
            // Encontrar o Produto
            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.ProdutoID == id);


            // Caso nao encontre
            if (produto == null) return;

            // Caso eu encontre
            produto.Preco = novoPreco;

            await _context.SaveChangesAsync();
        }

        public async Task AtualizarStatus(int id, bool novoStatus)
        {
            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.ProdutoID == id);

            if (produto == null) return;

            produto.Status = novoStatus;

            await _context.SaveChangesAsync();
        }

        public async Task AtualizarProduto(Produto produtoAtualizado)
        {
            _context.Produtos.Update(produtoAtualizado);

            await _context.SaveChangesAsync();
        }

        public async Task RemoverProduto(int id)
        {
            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.ProdutoID == id);

            if (produto == null) return;

            // Se eu encontrar - excluo o produto
            _context.Produtos.Remove(produto);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> VerificarSeNomeExiste(string nome)
        {
            return await _context.Produtos.AnyAsync(p => p.Nome == nome);
        }

        public async Task<int> ContarProdutos()
        {
            return await _context.Produtos.CountAsync(p => p.Status);
        }

        public async Task<List<Produto>> ObterLanchesPremium()
        {
            return await _context.Produtos
                .Where(p => p.Preco > 50)
                // .AsNoTracking()
                .ToListAsync();
        }

    }
}