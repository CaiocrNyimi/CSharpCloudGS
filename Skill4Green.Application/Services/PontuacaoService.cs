using AutoMapper;
using Microsoft.Extensions.Logging;
using Skill4Green.Application.DTOs;
using Skill4Green.Application.Interfaces;
using Skill4Green.Domain.Entities;

namespace Skill4Green.Application.Services;

public class PontuacaoService : IPontuacaoService
{
    private readonly IPontuacaoRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<PontuacaoService> _logger;

    public PontuacaoService(IPontuacaoRepository repo, IMapper mapper, ILogger<PontuacaoService> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<PontuacaoDto>> ListarAsync(int pagina, int tamanho)
    {
        _logger.LogInformation("Listando pontuações - Página: {Pagina}, Tamanho: {Tamanho}", pagina, tamanho);
        var dados = await _repo.ListarPaginadoAsync(pagina, tamanho);
        return _mapper.Map<IEnumerable<PontuacaoDto>>(dados);
    }

    public async Task<PontuacaoDto?> ObterPorIdAsync(int id)
    {
        _logger.LogInformation("Buscando pontuação por ID: {Id}", id);
        var dado = await _repo.ObterPorIdAsync(id);
        return dado == null ? null : _mapper.Map<PontuacaoDto>(dado);
    }

    public async Task<PontuacaoDto> CriarAsync(CreatePontuacaoDto dto)
    {
        _logger.LogInformation("Criando nova pontuação para {Nome}", dto.Nome);

        var existente = await _repo.ObterPorNomeAsync(dto.Nome);
        if (existente != null)
        {
            _logger.LogWarning("Já existe uma pontuação com o nome {Nome}", dto.Nome);
            throw new InvalidOperationException("O nome informado já está em uso.");
        }

        var entidade = _mapper.Map<Pontuacao>(dto);
        await _repo.AdicionarAsync(entidade);
        _logger.LogInformation("Pontuação criada com sucesso: {@Entidade}", entidade);
        return _mapper.Map<PontuacaoDto>(entidade);
    }

    public async Task AtualizarAsync(int id, UpdatePontuacaoDto dto)
    {
        _logger.LogInformation("Atualizando pontuação ID: {Id}", id);
        var entidade = await _repo.ObterPorIdAsync(id);
        if (entidade == null)
        {
            _logger.LogWarning("Pontuação ID {Id} não encontrada", id);
            throw new KeyNotFoundException("Pontuação não encontrada");
        }

        var existente = await _repo.ObterPorNomeAsync(dto.Nome);
        if (existente != null && existente.Id != id)
        {
            _logger.LogWarning("Nome {Nome} já está em uso por outra pontuação", dto.Nome);
            throw new InvalidOperationException("Já existe outra pontuação com esse nome.");
        }

        _mapper.Map(dto, entidade);
        entidade.AtualizadoEm = DateTime.UtcNow;

        await _repo.AtualizarAsync(entidade);
        _logger.LogInformation("Pontuação ID {Id} atualizada com sucesso", id);
    }

    public async Task DeletarAsync(int id)
    {
        _logger.LogInformation("Removendo pontuação ID: {Id}", id);
        await _repo.RemoverAsync(id);
        _logger.LogInformation("Pontuação ID {Id} removida com sucesso", id);
    }
}