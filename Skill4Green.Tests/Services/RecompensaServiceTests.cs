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

public class RecompensaServiceTests
{
    private readonly Mock<IRecompensaRepository> _recompensaRepo = new();
    private readonly Mock<IPontuacaoRepository> _pontuacaoRepo = new();
    private readonly IMapper _mapper;
    private readonly RecompensaService _service;

    public RecompensaServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Recompensa, RecompensaDto>();
            cfg.CreateMap<CreateRecompensaDto, Recompensa>();
            cfg.CreateMap<UpdateRecompensaDto, Recompensa>();
        });

        _mapper = config.CreateMapper();
        _service = new RecompensaService(_recompensaRepo.Object, _pontuacaoRepo.Object, _mapper, Mock.Of<ILogger<RecompensaService>>());
    }

    [Fact]
    public async Task ListarAsync_DeveRetornarLista()
    {
        _recompensaRepo.Setup(r => r.ListarAsync())
            .ReturnsAsync(new List<Recompensa> { new() { Id = 1, Nome = "Copo", CustoEcoCoins = 10 } });

        var resultado = await _service.ListarAsync();

        Assert.Single(resultado);
        Assert.Equal("Copo", resultado.First().Nome);
    }

    [Fact]
    public async Task ObterPorIdAsync_DeveRetornarDto()
    {
        _recompensaRepo.Setup(r => r.ObterPorIdAsync(1))
            .ReturnsAsync(new Recompensa { Id = 1, Nome = "Caneca" });

        var resultado = await _service.ObterPorIdAsync(1);

        Assert.NotNull(resultado);
        Assert.Equal("Caneca", resultado?.Nome);
    }

    [Fact]
    public async Task CriarAsync_DeveRetornarDtoCriado()
    {
        var dto = new CreateRecompensaDto { Nome = "Squeeze", CustoEcoCoins = 20 };

        var resultado = await _service.CriarAsync(dto);

        Assert.Equal("Squeeze", resultado.Nome);
        Assert.Equal(20, resultado.CustoEcoCoins);
    }

    [Fact]
    public async Task TrocarAsync_ComSaldoInsuficiente_DeveLancarExcecao()
    {
        _recompensaRepo.Setup(r => r.ObterPorIdAsync(1))
            .ReturnsAsync(new Recompensa { Id = 1, Nome = "Copo", CustoEcoCoins = 100 });

        _pontuacaoRepo.Setup(r => r.ObterPorNomeAsync("Caio"))
            .ReturnsAsync(new Pontuacao { Nome = "Caio", EcoCoins = 50 });

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _service.TrocarAsync("Caio", 1));
    }

    [Fact]
    public async Task AtualizarAsync_ComIdInvalido_DeveLancarExcecao()
    {
        _recompensaRepo.Setup(r => r.ObterPorIdAsync(99)).ReturnsAsync((Recompensa?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _service.AtualizarAsync(99, new UpdateRecompensaDto()));
    }

    [Fact]
    public async Task DeletarAsync_DeveChamarRemover()
    {
        await _service.DeletarAsync(1);
        _recompensaRepo.Verify(r => r.RemoverAsync(1), Times.Once);
    }
}