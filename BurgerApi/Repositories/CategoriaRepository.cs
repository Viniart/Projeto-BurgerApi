using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerApi.Data;
using BurgerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerApi.Repositories
{
    public class CategoriaRepository
    {
        private AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        // Listar e Cadastrar
        public async Task<List<Categoria>> ListarCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task CadastrarCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);

            await _context.SaveChangesAsync();
        }

        public async Task RemoverCategoria(int id)
        {
            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.CategoriaID == id);

            if (categoria == null) return;

            _context.Categorias.Remove(categoria);

            await _context.SaveChangesAsync();
        }
    }
}