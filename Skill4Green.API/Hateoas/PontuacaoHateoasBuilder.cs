using Skill4Green.API.Hateoas;
using Skill4Green.Application.DTOs;

namespace Skill4Green.API.Hateoas;

public class PontuacaoHateoasBuilder
{
    public ResourceWrapper<PontuacaoDto> Build(PontuacaoDto dto)
    {
        var resource = new ResourceWrapper<PontuacaoDto>(dto);

        resource.Links.Add(new HateoasLink
        {
            Rel = "self",
            Href = $"/api/v1/pontuacoes/{dto.Id}",
            Method = "GET"
        });

        resource.Links.Add(new HateoasLink
        {
            Rel = "update",
            Href = $"/api/v1/pontuacoes/{dto.Id}",
            Method = "PUT"
        });

        resource.Links.Add(new HateoasLink
        {
            Rel = "delete",
            Href = $"/api/v1/pontuacoes/{dto.Id}",
            Method = "DELETE"
        });

        return resource;
    }
}