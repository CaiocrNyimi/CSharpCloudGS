using Xunit;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Skill4Green.Application.Services;
using Skill4Green.Application.Interfaces;
using Skill4Green.Domain.Entities;
using Skill4Green.Application.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;

public class PontuacaoServiceTests
{
    private readonly Mock<IPontuacaoRepository> _repoMock = new();
    private readonly IMapper _mapper;
    private readonly PontuacaoService _service;

    public PontuacaoServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Pontuacao, PontuacaoDto>();
            cfg.CreateMap<CreatePontuacaoDto, Pontuacao>();
            cfg.CreateMap<UpdatePontuacaoDto, Pontuacao>();
        });

        _mapper = config.CreateMapper();
        _service = new PontuacaoService(_repoMock.Object, _mapper, Mock.Of<ILogger<PontuacaoService>>());
    }

    [Fact]
    public async Task ListarAsync_DeveRetornarLista()
    {
        _repoMock.Setup(r => r.ListarPaginadoAsync(1, 10))
                 .ReturnsAsync(new List<Pontuacao> { new() { Id = 1, Nome = "Caio", EcoCoins = 100 } });

        var resultado = await _service.ListarAsync(1, 10);

        Assert.Single(resultado);
        Assert.Equal("Caio", resultado.First().Nome);
    }

    [Fact]
    public async Task ObterPorIdAsync_DeveRetornarDto()
    {
        _repoMock.Setup(r => r.ObterPorIdAsync(1))
                 .ReturnsAsync(new Pontuacao { Id = 1, Nome = "Caio" });

        var resultado = await _service.ObterPorIdAsync(1);

        Assert.NotNull(resultado);
        Assert.Equal("Caio", resultado?.Nome);
    }

    [Fact]
    public async Task CriarAsync_DeveRetornarDtoCriado()
    {
        var dto = new CreatePontuacaoDto { Nome = "Novo", EcoCoins = 50, NivelVerde = 1 };

        var resultado = await _service.CriarAsync(dto);

        Assert.Equal("Novo", resultado.Nome);
        Assert.Equal(50, resultado.EcoCoins);
    }

    [Fact]
    public async Task AtualizarAsync_ComIdInvalido_DeveLancarExcecao()
    {
        _repoMock.Setup(r => r.ObterPorIdAsync(99)).ReturnsAsync((Pontuacao?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _service.AtualizarAsync(99, new UpdatePontuacaoDto()));
    }

    [Fact]
    public async Task DeletarAsync_DeveChamarRemover()
    {
        await _service.DeletarAsync(1);
        _repoMock.Verify(r => r.RemoverAsync(1), Times.Once);
    }
}