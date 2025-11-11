using Skill4Green.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Skill4Green.API.Examples;

public class CreatePontuacaoDtoExample : IExamplesProvider<CreatePontuacaoDto>
{
    public CreatePontuacaoDto GetExamples() => new()
    {
        Nome = "caio123",
        EcoCoins = 100,
        NivelVerde = 2,
        AtualizadoEm = new DateTime(2025, 11, 11, 0, 30, 0)
    };
}