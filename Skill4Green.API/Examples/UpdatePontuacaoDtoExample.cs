using Skill4Green.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Skill4Green.API.Examples;

public class UpdatePontuacaoDtoExample : IExamplesProvider<UpdatePontuacaoDto>
{
    public UpdatePontuacaoDto GetExamples() => new()
    {
        Nome = "caio123",
        EcoCoins = 150,
        NivelVerde = 3,
        AtualizadoEm = new DateTime(2025, 11, 11, 1, 0, 0)
    };
}