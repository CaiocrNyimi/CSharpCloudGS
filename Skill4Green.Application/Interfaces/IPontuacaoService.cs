using Skill4Green.Application.DTOs;

namespace Skill4Green.Application.Interfaces;

public interface IPontuacaoService
{
    Task<IEnumerable<PontuacaoDto>> ListarAsync(int pagina, int tamanho);
    Task<PontuacaoDto?> ObterPorIdAsync(int id);
    Task<PontuacaoDto> CriarAsync(CreatePontuacaoDto dto);
    Task AtualizarAsync(int id, UpdatePontuacaoDto dto);
    Task DeletarAsync(int id);
}