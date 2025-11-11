using FluentValidation;
using Skill4Green.Application.DTOs;

public class CreatePontuacaoDtoValidator : AbstractValidator<CreatePontuacaoDto>
{
    public CreatePontuacaoDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres");

        RuleFor(x => x.EcoCoins)
            .GreaterThanOrEqualTo(0).WithMessage("EcoCoins não pode ser negativo");

        RuleFor(x => x.NivelVerde)
            .InclusiveBetween(1, 5).WithMessage("O nível verde deve estar entre 1 e 5");
    }
}