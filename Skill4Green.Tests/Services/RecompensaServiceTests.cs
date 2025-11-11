using Xunit;
using Moq;
using AutoMapper;
using Skill4Green.Application.Services;
using Skill4Green.Application.DTOs;
using Skill4Green.Application.Interfaces;
using Skill4Green.Domain.Entities;

namespace Skill4Green.Tests.Services;

public class RecompensaServiceTests
{
    [Fact]
    public async Task TrocarAsync_DeveReduzirEcoCoins_QuandoSaldoSuficiente()
    {
        var recompensa = new Recompensa { Id = 1, Nome = "Voucher", CustoEcoCoins = 10 };
        var pontuacao = new Pontuacao { Id = 1, ColaboradorId = "abc123", EcoCoins = 20 };

        var mockRecompensaRepo = new Mock<IRecompensaRepository>();
        mockRecompensaRepo.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(recompensa);

        var mockPontuacaoRepo = new Mock<IPontuacaoRepository>();
        mockPontuacaoRepo.Setup(p => p.ObterPorColaboradorIdAsync("abc123")).ReturnsAsync(pontuacao);
        mockPontuacaoRepo.Setup(p => p.AtualizarAsync(It.IsAny<Pontuacao>())).Returns(Task.CompletedTask);

        var mockMapper = new Mock<IMapper>();

        var service = new RecompensaService(mockRecompensaRepo.Object, mockPontuacaoRepo.Object, mockMapper.Object);
        var resultado = await service.TrocarAsync("abc123", 1);

        Assert.True(resultado);
        Assert.Equal(10, pontuacao.EcoCoins);
    }

    [Fact]
    public async Task TrocarAsync_DeveRetornarFalse_QuandoSaldoInsuficiente()
    {
        var recompensa = new Recompensa { Id = 1, Nome = "Voucher", CustoEcoCoins = 50 };
        var pontuacao = new Pontuacao { Id = 1, ColaboradorId = "abc123", EcoCoins = 20 };

        var mockRecompensaRepo = new Mock<IRecompensaRepository>();
        mockRecompensaRepo.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(recompensa);

        var mockPontuacaoRepo = new Mock<IPontuacaoRepository>();
        mockPontuacaoRepo.Setup(p => p.ObterPorColaboradorIdAsync("abc123")).ReturnsAsync(pontuacao);

        var mockMapper = new Mock<IMapper>();

        var service = new RecompensaService(mockRecompensaRepo.Object, mockPontuacaoRepo.Object, mockMapper.Object);
        var resultado = await service.TrocarAsync("abc123", 1);

        Assert.False(resultado);
        Assert.Equal(20, pontuacao.EcoCoins);
    }
}