using Skill4Green.API.Hateoas;
using Skill4Green.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Skill4Green.API.Hateoas;

public class PontuacaoHateoasBuilder
{
    public ResourceWrapper<PontuacaoDto> Build(PontuacaoDto dto, IUrlHelper url)
    {
        var resource = new ResourceWrapper<PontuacaoDto>(dto);

        resource.Links.Add(new HateoasLink
        {
            Rel = "self",
            Href = url.Link("GetPontuacaoById", new { id = dto.Id }) ?? $"/api/v1/pontuacoes/{dto.Id}",
            Method = "GET"
        });

        resource.Links.Add(new HateoasLink
        {
            Rel = "update",
            Href = url.Link("GetPontuacaoById", new { id = dto.Id }) ?? $"/api/v1/pontuacoes/{dto.Id}",
            Method = "PUT"
        });

        resource.Links.Add(new HateoasLink
        {
            Rel = "delete",
            Href = url.Link("GetPontuacaoById", new { id = dto.Id }) ?? $"/api/v1/pontuacoes/{dto.Id}",
            Method = "DELETE"
        });

        resource.Links.Add(new HateoasLink
        {
            Rel = "collection",
            Href = url.Link("GetAllPontuacoes", null) ?? "/api/v1/pontuacoes",
            Method = "GET"
        });

        return resource;
    }
}