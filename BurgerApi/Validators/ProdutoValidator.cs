using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerApi.DTOs;
using FluentValidation;

namespace BurgerApi.Validators
{
    public class ProdutoValidator : AbstractValidator<ProdutoRequisicaoDTO>
    {
        public ProdutoValidator()
        {
            // Regras de Validacao
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome do produto é obrigatório.");

            RuleFor(x => x.Preco)
                .GreaterThan(0)
                .WithMessage("O preco deve ser maior do que zero.");
        }
    }
}