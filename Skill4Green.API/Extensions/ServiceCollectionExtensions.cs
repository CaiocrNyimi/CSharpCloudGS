using Skill4Green.Application.Interfaces;
using Skill4Green.Application.Services;
using Skill4Green.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Skill4Green.Infrastructure.Repositories;

namespace Skill4Green.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSkill4GreenServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<Skill4GreenDbContext>(options =>
            options.UseOracle(config.GetConnectionString("OracleConnection")));

        services.AddScoped<IPontuacaoRepository, PontuacaoRepository>();
        services.AddScoped<IRecompensaRepository, RecompensaRepository>();
        services.AddScoped<IPontuacaoService, PontuacaoService>();
        services.AddScoped<IRecompensaService, RecompensaService>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddHealthChecks().AddDbContextCheck<Skill4GreenDbContext>();

        return services;
    }
}