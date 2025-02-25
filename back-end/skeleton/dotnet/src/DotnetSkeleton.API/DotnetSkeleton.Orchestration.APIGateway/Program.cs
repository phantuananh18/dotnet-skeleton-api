using Asp.Versioning;
using Microsoft.OpenApi.Models;
using System.Reflection;
using DotnetSkeleton.Orchestration.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
// Configure API versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Dotnet Skeleton API",
        Version = "v1",
        Description = "This is a simple Skeleton API made with .NET 8.0 and documented with Swagger.",
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
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
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

var app = builder.Build();

app.MapDefaultEndpoints();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.MapReverseProxy();

app.Run();
