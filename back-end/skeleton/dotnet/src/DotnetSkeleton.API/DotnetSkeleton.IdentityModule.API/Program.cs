using Asp.Versioning;
using DotnetSkeleton.IdentityModule.API.Extensions;
using DotnetSkeleton.IdentityModule.Application;
using DotnetSkeleton.IdentityModule.Domain.Resources;
using DotnetSkeleton.IdentityModule.Infrastructure;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Extensions;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;
using DotnetSkeleton.Orchestration.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection(Constant.SystemInfo.Logging));
builder.Logging.AddConsole();
builder.Logging.AddFilter("Microsoft", LogLevel.Warning);
builder.Logging.AddFilter("System", LogLevel.Error);

// Configure the API behavior options
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new ToKebabParameterTransformer()));
    options.EnableEndpointRouting = false;
})
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    })
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (_, factory) => factory.Create(typeof(Resources));
    });

// Configure API versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

// Configure the CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsPolicy =>
        corsPolicy
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(_ => true)
            .AllowCredentials());
});

// Read the configuration and configure the services
builder.Host.ConfigureServices((context, services) =>
{
    var config = context.Configuration;
    services.Configure<DatabaseOptions>(config.GetSection(DatabaseOptions.JsonKey));
    services.Configure<TokenOptions>(config.GetSection(TokenOptions.JsonKey));
    services.Configure<EncryptOptions>(config.GetSection(EncryptOptions.JsonKey));
    services.Configure<SystemInfoOptions>(config.GetSection(SystemInfoOptions.JsonKey));
    services.Configure<MailOptions>(config.GetSection(MailOptions.JsonKey));
    services.Configure<LocalizationOptions>(config.GetSection(LocalizationOptions.JsonKey));
    services.Configure<HttpClientRetryOptions>(config.GetSection(HttpClientRetryOptions.JsonKey));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

//Add Identity Module Service
builder.Services.AddIdentityModuleInfrastructure();
builder.Services.AddIdentityModuleApplication(builder.Configuration);

/*
 * Start the application
 */

var app = builder.Build();

app.MapDefaultEndpoints();
app.Logger.LogInformation("[DC8-Framework/@Identity-Module] Starting the application!!!");

if (app.Environment.IsEnvironment("Debug"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization(app.Services.GetService<IOptions<RequestLocalizationOptions>>()!.Value);
app.UseCors();
app.MapControllers();

app.Run();