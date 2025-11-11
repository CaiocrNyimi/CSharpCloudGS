using Skill4Green.API.Hateoas;
using Skill4Green.Application.DTOs;

namespace Skill4Green.API.Hateoas;

public class RecompensaHateoasBuilder
{
    public ResourceWrapper<RecompensaDto> Build(RecompensaDto dto)
    {
        var resource = new ResourceWrapper<RecompensaDto>(dto);

        resource.Links.Add(new HateoasLink
        {
            Rel = "self",
            Href = $"/api/v1/recompensas/{dto.Id}",
            Method = "GET"
        });

        resource.Links.Add(new HateoasLink
        {
            Rel = "update",
            Href = $"/api/v1/recompensas/{dto.Id}",
            Method = "PUT"
        });

        resource.Links.Add(new HateoasLink
        {
            Rel = "delete",
            Href = $"/api/v1/recompensas/{dto.Id}",
            Method = "DELETE"
        });

        resource.Links.Add(new HateoasLink
        {
            Rel = "trocar",
            Href = $"/api/v1/recompensas/trocar?nome={{nome}}&recompensaId={dto.Id}",
            Method = "POST"
        });

        return resource;
    }
}