using AutoMapper;
using Skill4Green.Application.DTOs;
using Skill4Green.Domain.Entities;

namespace Skill4Green.API.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Pontuacao, PontuacaoDto>().ReverseMap();
        CreateMap<Recompensa, RecompensaDto>().ReverseMap();
    }
}