using FluentValidation;
using Skill4Green.Application.DTOs;

public class CreateRecompensaDtoValidator : AbstractValidator<CreateRecompensaDto>
{
    public CreateRecompensaDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres");

        RuleFor(x => x.CustoEcoCoins)
            .GreaterThan(0).WithMessage("O custo deve ser maior que zero");
    }
}