using AutoMapper;
using Skill4Green.Domain.Entities;
using Skill4Green.Application.DTOs;

namespace Skill4Green.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Pontuacao, PontuacaoDto>().ReverseMap();
        CreateMap<CreatePontuacaoDto, Pontuacao>();
        CreateMap<UpdatePontuacaoDto, Pontuacao>();

        CreateMap<Recompensa, RecompensaDto>().ReverseMap();
        CreateMap<CreateRecompensaDto, Recompensa>();
        CreateMap<UpdateRecompensaDto, Recompensa>();
    }
}