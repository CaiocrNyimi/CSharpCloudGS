using Microsoft.EntityFrameworkCore;
using Skill4Green.Domain.Entities;

namespace Skill4Green.Infrastructure.Data;

public class Skill4GreenDbContext : DbContext
{
    public Skill4GreenDbContext(DbContextOptions<Skill4GreenDbContext> options)
        : base(options)
    {
    }

    public DbSet<Pontuacao> Pontuacoes => Set<Pontuacao>();
    public DbSet<Recompensa> Recompensas => Set<Recompensa>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pontuacao>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Nome).IsRequired();
            entity.Property(p => p.EcoCoins).IsRequired();
            entity.Property(p => p.NivelVerde).IsRequired();
            entity.Property(p => p.AtualizadoEm).IsRequired();
        });

        modelBuilder.Entity<Recompensa>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Nome).IsRequired();
            entity.Property(r => r.CustoEcoCoins).IsRequired();
        });
    }
}