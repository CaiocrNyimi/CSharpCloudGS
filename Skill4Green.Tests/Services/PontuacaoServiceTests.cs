using Xunit;
using Moq;
using AutoMapper;
using Skill4Green.Application.Services;
using Skill4Green.Application.DTOs;
using Skill4Green.Application.Interfaces;
using Skill4Green.Domain.Entities;

namespace Skill4Green.Tests.Services;

public class PontuacaoServiceTests
{
    [Fact]
    public async Task CriarAsync_DeveRetornarDtoComEcoCoins()
    {
        var dto = new PontuacaoDto { ColaboradorId = "abc123", EcoCoins = 10, NivelVerde = 2 };
        var entidade = new Pontuacao { ColaboradorId = "abc123", EcoCoins = 10, NivelVerde = 2 };

        var mockRepo = new Mock<IPontuacaoRepository>();
        mockRepo.Setup(r => r.AdicionarAsync(It.IsAny<Pontuacao>())).Returns(Task.CompletedTask);

        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(m => m.Map<Pontuacao>(dto)).Returns(entidade);
        mockMapper.Setup(m => m.Map<PontuacaoDto>(entidade)).Returns(dto);

        var service = new PontuacaoService(mockRepo.Object, mockMapper.Object);
        var result = await service.CriarAsync(dto);

        Assert.Equal(10, result.EcoCoins);
        Assert.Equal("abc123", result.ColaboradorId);
    }

    [Fact]
    public async Task AtualizarAsync_DeveAlterarEcoCoinsENivel()
    {
        var dto = new PontuacaoDto { ColaboradorId = "abc123", EcoCoins = 20, NivelVerde = 3 };
        var entidade = new Pontuacao { Id = 1, ColaboradorId = "abc123", EcoCoins = 10, NivelVerde = 1 };

        var mockRepo = new Mock<IPontuacaoRepository>();
        mockRepo.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(entidade);
        mockRepo.Setup(r => r.AtualizarAsync(It.IsAny<Pontuacao>())).Returns(Task.CompletedTask);

        var mockMapper = new Mock<IMapper>();

        var service = new PontuacaoService(mockRepo.Object, mockMapper.Object);
        await service.AtualizarAsync(1, dto);

        Assert.Equal(20, entidade.EcoCoins);
        Assert.Equal(3, entidade.NivelVerde);
    }
}