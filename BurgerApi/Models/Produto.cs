using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BurgerApi.Models
{
    public class Produto
    {
        public int ProdutoID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public string Imagem { get; set; }
        public decimal Preco { get; set; }
        public bool Status { get; set; }

        // Over-fetching

        // 1 para Muitos
        // ? - Opcional (C# - Banco)
        public int? CategoriaID { get; set; }
        public Categoria? Categoria { get; set; }

        // public int QuantidadeEmEstoque { get; set; }

        // public bool TemEstoque()
        // {
        //     return QuantidadeEmEstoque > 0;
        // }

        public void AplicarDesconto(decimal porcentagem)
        {
            if (porcentagem < 0 || porcentagem > 100)
            {
                throw new ArgumentException("O desconto deve estar entre 0 e 100");
            }

            Preco -= Preco * (porcentagem / 100);
        }
    }
}