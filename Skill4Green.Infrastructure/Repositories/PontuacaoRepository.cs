using Skill4Green.Application.Interfaces;
using Skill4Green.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Skill4Green.Infrastructure.Data;

namespace Skill4Green.Infrastructure.Repositories;

public class PontuacaoRepository : IPontuacaoRepository
{
    private readonly Skill4GreenDbContext _context;

    public PontuacaoRepository(Skill4GreenDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pontuacao>> ListarPaginadoAsync(int pagina, int tamanho) =>
        await _context.Pontuacoes
            .OrderBy(p => p.AtualizadoEm)
            .Skip((pagina - 1) * tamanho)
            .Take(tamanho)
            .ToListAsync();

    public async Task<Pontuacao?> ObterPorIdAsync(int id) =>
        await _context.Pontuacoes.FindAsync(id);

    public async Task<Pontuacao?> ObterPorNomeAsync(string nome) =>
        await _context.Pontuacoes.FirstOrDefaultAsync(p => p.Nome.ToLower() == nome.Trim().ToLower());

    public async Task AdicionarAsync(Pontuacao pontuacao)
    {
        _context.Pontuacoes.Add(pontuacao);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Pontuacao pontuacao)
    {
        _context.Pontuacoes.Update(pontuacao);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(int id)
    {
        var entidade = await _context.Pontuacoes.FindAsync(id);
        if (entidade != null)
        {
            _context.Pontuacoes.Remove(entidade);
            await _context.SaveChangesAsync();
        }
    }
}