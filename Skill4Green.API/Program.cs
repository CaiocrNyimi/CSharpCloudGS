using Microsoft.EntityFrameworkCore;
using Skill4Green.Application.Interfaces;
using Skill4Green.Application.Services;
using Skill4Green.Infrastructure.Data;
using Skill4Green.Infrastructure.Repositories;
using Skill4Green.Application.Mappings;
using Swashbuckle.AspNetCore.Filters;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

// 🔧 Configurações básicas
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.ExampleFilters();

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// 🔌 Banco de dados Oracle
builder.Services.AddDbContext<Skill4GreenDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🧩 AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// 💉 Injeção de dependência
builder.Services.AddScoped<IPontuacaoService, PontuacaoService>();
builder.Services.AddScoped<IRecompensaService, RecompensaService>();
builder.Services.AddScoped<IPontuacaoRepository, PontuacaoRepository>();
builder.Services.AddScoped<IRecompensaRepository, RecompensaRepository>();

// 📦 Exemplos para Swagger
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

// 🩺 Health Check
builder.Services.AddHealthChecks()
    .AddDbContextCheck<Skill4GreenDbContext>("Banco Oracle");

// 🔍 Tracing com OpenTelemetry
builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Skill4Green.API"))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddConsoleExporter();
    });

var app = builder.Build();

// 🌐 Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

// 🩺 Endpoint de Health Check
app.MapHealthChecks("/health");

app.Run();