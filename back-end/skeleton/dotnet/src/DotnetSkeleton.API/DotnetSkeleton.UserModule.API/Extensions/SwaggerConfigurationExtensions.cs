using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DotnetSkeleton.UserModule.API.Extensions;

public static class SwaggerConfigurationExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Dotnet Skeleton API - User Module",
                Version = "v1",
                Description = "User Service APIs.",
                TermsOfService = new Uri("http://11.11.7.86:8080/secure/RapidBoard.jspa?rapidView=456"),
                Contact = new()
                {
                    Email = "dc8framework@gmail.com",
                    Name = "Contact Us",
                    Url = new Uri("http://11.11.7.86:8080/secure/RapidBoard.jspa?rapidView=456")
                },
                License = new()
                {
                    Name = "License",
                    Url = new Uri("http://11.11.7.86:8080/secure/RapidBoard.jspa?rapidView=456")
                }
            });

            c.DescribeAllParametersInCamelCase();
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });

            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });

        return services;
    }
}