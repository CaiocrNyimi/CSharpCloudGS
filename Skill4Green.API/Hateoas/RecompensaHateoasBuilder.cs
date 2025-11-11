using Skill4Green.API.Hateoas;
using Skill4Green.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Skill4Green.API.Hateoas;

public class RecompensaHateoasBuilder
{
    public ResourceWrapper<RecompensaDto> Build(RecompensaDto dto, IUrlHelper url)
    {
        var resource = new ResourceWrapper<RecompensaDto>(dto);

        resource.Links.Add(new HateoasLink
        {
            Rel = "self",
            Href = url.Link("GetRecompensaById", new { id = dto.Id }) ?? $"/api/v1/recompensas/{dto.Id}",
            Method = "GET"
        });

        resource.Links.Add(new HateoasLink
        {
            Rel = "update",
            Href = url.Link("GetRecompensaById", new { id = dto.Id }) ?? $"/api/v1/recompensas/{dto.Id}",
            Method = "PUT"
        });

        resource.Links.Add(new HateoasLink
        {
            Rel = "delete",
            Href = url.Link("GetRecompensaById", new { id = dto.Id }) ?? $"/api/v1/recompensas/{dto.Id}",
            Method = "DELETE"
        });

        var trocarHref = url.Link("GetRecompensaById", new { id = dto.Id })?.Replace($"/{dto.Id}", "/trocar");
        resource.Links.Add(new HateoasLink
        {
            Rel = "trocar",
            Href = (trocarHref ?? "/api/v1/recompensas/trocar") + $"?nome={{nome}}&recompensaId={dto.Id}",
            Method = "POST"
        });

        resource.Links.Add(new HateoasLink
        {
            Rel = "collection",
            Href = url.Link("GetAllRecompensas", null) ?? "/api/v1/recompensas",
            Method = "GET"
        });

        return resource;
    }
}