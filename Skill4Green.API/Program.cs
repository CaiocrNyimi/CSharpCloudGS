using Microsoft.EntityFrameworkCore;
using Skill4Green.Application.Interfaces;
using Skill4Green.Application.Services;
using Skill4Green.Infrastructure.Data;
using Skill4Green.Infrastructure.Repositories;
using Skill4Green.Application.Mappings;
using Swashbuckle.AspNetCore.Filters;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

// 🔧 Configurações básicas
builder.Services.AddControllers();
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = false;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.ExampleFilters();

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// 🔌 Banco de dados SQL Server
builder.Services.AddDbContext<Skill4GreenDbContext>(options =>
    options.UseSqlServer(connectionString));

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
    .AddDbContextCheck<Skill4GreenDbContext>("Banco SQL Server");

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

// 🧭 Versionamento da API
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

// 🌐 Middlewares
app.UseSwagger();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwaggerUI(options =>
{
    foreach (var desc in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", $"Skill4Green API {desc.GroupName}");
    }
});

app.UseAuthorization();
app.MapControllers();

// 🩺 Endpoint de Health Check
app.MapHealthChecks("/health");

app.MapGet("/nova-rota", () => Results.Text("Rota adicionada"))
      .WithName("Rota Nova")
      .WithTags("Health")
      .Produces(StatusCodes.Status200OK);


app.Run();