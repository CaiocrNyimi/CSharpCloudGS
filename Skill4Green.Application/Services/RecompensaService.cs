using AutoMapper;
using Microsoft.Extensions.Logging;
using Skill4Green.Application.DTOs;
using Skill4Green.Application.Interfaces;
using Skill4Green.Domain.Entities;

namespace Skill4Green.Application.Services;

public class RecompensaService : IRecompensaService
{
    private readonly IRecompensaRepository _recompensas;
    private readonly IPontuacaoRepository _pontuacoes;
    private readonly IMapper _mapper;
    private readonly ILogger<RecompensaService> _logger;

    public RecompensaService(
        IRecompensaRepository recompensas,
        IPontuacaoRepository pontuacoes,
        IMapper mapper,
        ILogger<RecompensaService> logger)
    {
        _recompensas = recompensas;
        _pontuacoes = pontuacoes;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<RecompensaDto>> ListarAsync()
    {
        _logger.LogInformation("Listando recompensas disponíveis");
        var dados = await _recompensas.ListarAsync();
        return _mapper.Map<IEnumerable<RecompensaDto>>(dados);
    }

    public async Task<RecompensaDto?> ObterPorIdAsync(int id)
    {
        _logger.LogInformation("Buscando recompensa por ID: {Id}", id);
        var dado = await _recompensas.ObterPorIdAsync(id);
        return dado == null ? null : _mapper.Map<RecompensaDto>(dado);
    }

    public async Task<RecompensaDto> CriarAsync(CreateRecompensaDto dto)
    {
        _logger.LogInformation("Criando nova recompensa: {Nome}", dto.Nome);
        var entidade = _mapper.Map<Recompensa>(dto);
        await _recompensas.AdicionarAsync(entidade);
        _logger.LogInformation("Recompensa criada com sucesso: {@Entidade}", entidade);
        return _mapper.Map<RecompensaDto>(entidade);
    }

    public async Task<string> TrocarAsync(string nome, int recompensaId)
    {
        _logger.LogInformation("Tentando trocar recompensa ID {Id} para colaborador {Nome}", recompensaId, nome);

        var recompensa = await _recompensas.ObterPorIdAsync(recompensaId);
        if (recompensa == null)
        {
            _logger.LogWarning("Recompensa ID {Id} não encontrada", recompensaId);
            throw new ArgumentException("Recompensa não encontrada.");
        }

        var pontuacao = await _pontuacoes.ObterPorNomeAsync(nome);
        if (pontuacao == null)
        {
            _logger.LogWarning("Colaborador {Nome} não encontrado", nome);
            throw new ArgumentException("Colaborador não encontrado.");
        }

        if (pontuacao.EcoCoins < recompensa.CustoEcoCoins)
        {
            _logger.LogWarning("Colaborador {Nome} não possui EcoCoins suficientes", nome);
            throw new InvalidOperationException("Saldo insuficiente para trocar a recompensa.");
        }

        pontuacao.EcoCoins -= recompensa.CustoEcoCoins;
        await _pontuacoes.AtualizarAsync(pontuacao);

        _logger.LogInformation("Recompensa '{Recompensa}' trocada com sucesso por {Nome}", recompensa.Nome, nome);
        return $"Recompensa '{recompensa.Nome}' trocada com sucesso.";
    }

    public async Task AtualizarAsync(int id, UpdateRecompensaDto dto)
    {
        _logger.LogInformation("Atualizando recompensa ID: {Id}", id);
        var entidade = await _recompensas.ObterPorIdAsync(id);
        if (entidade == null)
        {
            _logger.LogWarning("Recompensa ID {Id} não encontrada", id);
            throw new KeyNotFoundException("Recompensa não encontrada");
        }

        _mapper.Map(dto, entidade);
        await _recompensas.AtualizarAsync(entidade);
        _logger.LogInformation("Recompensa ID {Id} atualizada com sucesso", id);
    }

    public async Task DeletarAsync(int id)
    {
        _logger.LogInformation("Removendo recompensa ID: {Id}", id);
        await _recompensas.RemoverAsync(id);
        _logger.LogInformation("Recompensa ID {Id} removida com sucesso", id);
    }
}