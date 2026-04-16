using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerApi.DTOs
{
    public class ProdutoRequisicaoDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        // public string Imagem { get; set; }
        public int CategoriaId { get; set; }
    }
}