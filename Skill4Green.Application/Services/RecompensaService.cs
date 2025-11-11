using AutoMapper;
using Skill4Green.Application.DTOs;
using Skill4Green.Application.Interfaces;
using Skill4Green.Domain.Entities;

namespace Skill4Green.Application.Services;

public class RecompensaService : IRecompensaService
{
    private readonly IRecompensaRepository _recompensas;
    private readonly IPontuacaoRepository _pontuacoes;
    private readonly IMapper _mapper;

    public RecompensaService(IRecompensaRepository recompensas, IPontuacaoRepository pontuacoes, IMapper mapper)
    {
        _recompensas = recompensas;
        _pontuacoes = pontuacoes;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RecompensaDto>> ListarAsync()
    {
        var dados = await _recompensas.ListarAsync();
        return _mapper.Map<IEnumerable<RecompensaDto>>(dados);
    }

    public async Task<RecompensaDto?> ObterPorIdAsync(int id)
    {
        var dado = await _recompensas.ObterPorIdAsync(id);
        return dado == null ? null : _mapper.Map<RecompensaDto>(dado);
    }

    public async Task<RecompensaDto> CriarAsync(CreateRecompensaDto dto)
    {
        var entidade = _mapper.Map<Recompensa>(dto);
        await _recompensas.AdicionarAsync(entidade);
        return _mapper.Map<RecompensaDto>(entidade);
    }

    public async Task<string> TrocarAsync(string nome, int recompensaId)
    {
        var recompensa = await _recompensas.ObterPorIdAsync(recompensaId);
        if (recompensa == null)
            throw new ArgumentException("Recompensa não encontrada.");
    
        var pontuacao = await _pontuacoes.ObterPorNomeAsync(nome);
        if (pontuacao == null)
            throw new ArgumentException("Colaborador não encontrado.");
    
        if (pontuacao.EcoCoins < recompensa.CustoEcoCoins)
            throw new InvalidOperationException("Saldo insuficiente para trocar a recompensa.");
    
        pontuacao.EcoCoins -= recompensa.CustoEcoCoins;
        await _pontuacoes.AtualizarAsync(pontuacao);
    
        return $"Recompensa '{recompensa.Nome}' trocada com sucesso.";
    }

    public async Task AtualizarAsync(int id, UpdateRecompensaDto dto)
    {
        var entidade = await _recompensas.ObterPorIdAsync(id);
        if (entidade == null) throw new KeyNotFoundException("Recompensa não encontrada");

        _mapper.Map(dto, entidade);
        await _recompensas.AtualizarAsync(entidade);
    }

    public async Task DeletarAsync(int id)
    {
        await _recompensas.RemoverAsync(id);
    }
}