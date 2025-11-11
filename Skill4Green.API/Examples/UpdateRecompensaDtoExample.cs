using Skill4Green.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Skill4Green.API.Examples;

public class UpdateRecompensaDtoExample : IExamplesProvider<UpdateRecompensaDto>
{
    public UpdateRecompensaDto GetExamples() => new()
    {
        Nome = "Copo térmico reutilizável",
        CustoEcoCoins = 60,
        Tipo = "Sustentável"
    };
}