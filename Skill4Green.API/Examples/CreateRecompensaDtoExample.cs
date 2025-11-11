using Skill4Green.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Skill4Green.API.Examples;

public class CreateRecompensaDtoExample : IExamplesProvider<CreateRecompensaDto>
{
    public CreateRecompensaDto GetExamples() => new()
    {
        Nome = "Copo reutilizável",
        CustoEcoCoins = 50,
        Tipo = "Sustentável"
    };
}