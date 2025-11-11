using Microsoft.EntityFrameworkCore;
using Skill4Green.Application.Interfaces;
using Skill4Green.Domain.Entities;
using Skill4Green.Infrastructure.Data;

namespace Skill4Green.Infrastructure.Repositories;

public class RecompensaRepository : IRecompensaRepository
{
    private readonly Skill4GreenDbContext _context;

    public RecompensaRepository(Skill4GreenDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Recompensa>> ListarAsync()
    {
        return await _context.Recompensas
            .OrderBy(r => r.Nome)
            .ToListAsync();
    }

    public async Task<Recompensa?> ObterPorIdAsync(int id)
    {
        return await _context.Recompensas.FindAsync(id);
    }

    public async Task AdicionarAsync(Recompensa recompensa)
    {
        await _context.Recompensas.AddAsync(recompensa);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Recompensa recompensa)
    {
        _context.Recompensas.Update(recompensa);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(int id)
    {
        var entidade = await _context.Recompensas.FindAsync(id);
        if (entidade != null)
        {
            _context.Recompensas.Remove(entidade);
            await _context.SaveChangesAsync();
        }
    }
}