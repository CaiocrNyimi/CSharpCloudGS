using Skill4Green.Domain.Entities;

namespace Skill4Green.Application.Interfaces;

public interface IRecompensaRepository
{
    Task<IEnumerable<Recompensa>> ListarAsync();
    Task<Recompensa?> ObterPorIdAsync(int id);
    Task AdicionarAsync(Recompensa recompensa);
    Task AtualizarAsync(Recompensa recompensa);
    Task RemoverAsync(int id);
}