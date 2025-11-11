using Skill4Green.Application.DTOs;

namespace Skill4Green.Application.Interfaces;

public interface IRecompensaService
{
    Task<IEnumerable<RecompensaDto>> ListarAsync();
    Task<RecompensaDto?> ObterPorIdAsync(int id);
    Task<RecompensaDto> CriarAsync(CreateRecompensaDto dto);
    Task<string> TrocarAsync(string nome, int recompensaId); // ✅ atualizado
    Task AtualizarAsync(int id, UpdateRecompensaDto dto);
    Task DeletarAsync(int id);
}