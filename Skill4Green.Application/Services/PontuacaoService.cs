using AutoMapper;
using Skill4Green.Application.DTOs;
using Skill4Green.Application.Interfaces;
using Skill4Green.Domain.Entities;

namespace Skill4Green.Application.Services;

public class PontuacaoService : IPontuacaoService
{
    private readonly IPontuacaoRepository _repo;
    private readonly IMapper _mapper;

    public PontuacaoService(IPontuacaoRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PontuacaoDto>> ListarAsync(int pagina, int tamanho)
    {
        var dados = await _repo.ListarPaginadoAsync(pagina, tamanho);
        return _mapper.Map<IEnumerable<PontuacaoDto>>(dados);
    }

    public async Task<PontuacaoDto?> ObterPorIdAsync(int id)
    {
        var dado = await _repo.ObterPorIdAsync(id);
        return dado == null ? null : _mapper.Map<PontuacaoDto>(dado);
    }

    public async Task<PontuacaoDto> CriarAsync(CreatePontuacaoDto dto)
    {
        var entidade = _mapper.Map<Pontuacao>(dto);
        await _repo.AdicionarAsync(entidade);
        return _mapper.Map<PontuacaoDto>(entidade);
    }

    public async Task AtualizarAsync(int id, UpdatePontuacaoDto dto)
    {
        var entidade = await _repo.ObterPorIdAsync(id);
        if (entidade == null) throw new KeyNotFoundException("Pontuação não encontrada");

        _mapper.Map(dto, entidade);
        entidade.AtualizadoEm = DateTime.UtcNow;

        await _repo.AtualizarAsync(entidade);
    }

    public async Task DeletarAsync(int id)
    {
        await _repo.RemoverAsync(id);
    }
}