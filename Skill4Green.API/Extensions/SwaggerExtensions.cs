using Microsoft.OpenApi.Models;

namespace Skill4Green.API.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSkill4GreenSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Skill4Green API", Version = "v1" });
            c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "X-API-KEY",
                Type = SecuritySchemeType.ApiKey,
                Description = "Chave de acesso da API"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme { Name = "ApiKey", Type = SecuritySchemeType.ApiKey, In = ParameterLocation.Header },
                    new List<string>()
                }
            });
        });

        return services;
    }
}