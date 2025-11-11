using Skill4Green.Domain.Entities;

namespace Skill4Green.Application.Interfaces;

public interface IPontuacaoRepository
{
    Task<IEnumerable<Pontuacao>> ListarPaginadoAsync(int pagina, int tamanho);
    Task<Pontuacao?> ObterPorIdAsync(int id);
    Task<Pontuacao?> ObterPorNomeAsync(string nome);
    Task AdicionarAsync(Pontuacao pontuacao);
    Task AtualizarAsync(Pontuacao pontuacao);
    Task RemoverAsync(int id);
}