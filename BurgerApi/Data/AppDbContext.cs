using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
        {
            
        }

        // Tabelas do Banco de Dados
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Nome da Tabela
            modelBuilder.Entity<Produto>().ToTable("TB_Produtos");

            // Chave Primaria
            modelBuilder.Entity<Produto>().HasKey(p => p.ProdutoID);

            // Colunas
            modelBuilder.Entity<Produto>().Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);


            modelBuilder.Entity<Produto>().Property(p => p.Descricao)
                .HasMaxLength(250);

            modelBuilder.Entity<Produto>().Property(p => p.Status)
                .HasDefaultValue(true);

            modelBuilder.Entity<Produto>().Property(p => p.Preco)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Produto>()
                // 1 Produto tem uma Categoria
                // Has One - Apenas 1
                // Has Many - Tem muitos
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaID);


            base.OnModelCreating(modelBuilder);
        }

    }
}