using AutoMapper;
using DotnetSkeleton.API.Extensions;
using DotnetSkeleton.Core.Infrastructure;
using DotnetSkeleton.NotificationModule.Application;
using DotnetSkeleton.NotificationModule.Application.Services;
using DotnetSkeleton.NotificationModule.Infrastructure;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using Microsoft.AspNetCore.RateLimiting;
using System.Reflection;
using DotnetSkeleton.Core.Application;
using DotnetSkeleton.Orchestration.ServiceDefaults;
using DotnetSkeleton.Utils.RabbitMqService;
using Constant = DotnetSkeleton.SharedKernel.Utils.Constant;

/*
 * Configure the application
 */

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Configure logging
var configuration = builder.Configuration;
builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection(Constant.SystemInfo.Logging));
builder.Logging.AddConsole();
//builder.Logging.AddFilter("Microsoft", LogLevel.Warning);
//builder.Logging.AddFilter("System", LogLevel.Error);

// Configure the API behavior options
builder.Services.AddControllers(options =>
   {
       options.Conventions.Add(new RouteTokenTransformerConvention(new ControllerExtension.ToKebabParameterTransformer()));
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
        options.DataAnnotationLocalizerProvider = (_, factory) => factory.Create(typeof(DotnetSkeleton.Core.Domain.Resources.Resources));
    });

// Configure API versioning
builder.Services.AddApiVersioning(options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.ReportApiVersions = true;
    });

// Configure the rate limiting
var rateLimiterOptions = builder.Configuration.GetSection(RateLimitOptions.JsonKey).Get<RateLimitOptions>();
builder.Services.AddRateLimiter(_ => _
    .AddTokenBucketLimiter(policyName: Constant.SystemInfo.TokenBucketRateLimit, options =>
    {
        options.TokenLimit = rateLimiterOptions!.TokenBucketRateLimiter.TokenLimit;
        options.QueueProcessingOrder = rateLimiterOptions.TokenBucketRateLimiter.QueueProcessingOrder;
        options.QueueLimit = rateLimiterOptions.TokenBucketRateLimiter.QueueLimit;
        options.ReplenishmentPeriod = rateLimiterOptions.TokenBucketRateLimiter.ReplenishmentPeriod;
        options.TokensPerPeriod = rateLimiterOptions.TokenBucketRateLimiter.TokensPerPeriod;
        options.AutoReplenishment = rateLimiterOptions.TokenBucketRateLimiter.AutoReplenishment;
    }));

// Configure the localization
var localizationOptions = builder.Configuration.GetSection(LocalizationOptions.JsonKey).Get<LocalizationOptions>();
var supportedCultures = localizationOptions!
    .SupportedCultures.Select(culture => new CultureInfo(culture)).ToList()
    ?? new List<CultureInfo> { new(Constant.SupportedCulture.EnUs), new(Constant.SupportedCulture.EsEs) };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(localizationOptions.DefaultCulture);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
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
    services.Configure<RateLimitOptions>(config.GetSection(RateLimitOptions.JsonKey));
    services.Configure<LocalizationOptions>(config.GetSection(LocalizationOptions.JsonKey));
    services.Configure<RabbitMqOptions>(config.GetSection(RabbitMqOptions.JsonKey));
    services.Configure<SignalROptions>(config.GetSection(SignalROptions.JsonKey));
    services.Configure<HttpClientRetryOptions>(config.GetSection(HttpClientRetryOptions.JsonKey));
});

// Configuring Swagger/OpenAPI
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
        c.SchemaGeneratorOptions.SchemaIdSelector = type => type.FullName;
        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
            $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    });

/*
 * Register services
 */

// Add services to the container
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// Add Core Module Service
builder.Services.AddCoreModuleInfrastructure();
builder.Services.AddCoreModuleApplication(configuration);

//Add Notification Module Service
builder.Services.AddNotificationModuleInfrastructure();
builder.Services.AddNotificationModuleApplication(configuration);

// Add Queue Service Module
builder.Services.AddRabbitMqService();

// Assembly Scanning for auto configuration
var mappingConfig = new MapperConfiguration(cfg =>
    cfg.AddMaps(new[] {
        "DotnetSkeleton.UserModule.Application",
        "DotnetSkeleton.NotificationModule.Application",
        "DotnetSkeleton.EmailModule.Application",
        "DotnetSkeleton.Core.Application"
    })).CreateMapper();

builder.Services.AddSingleton(mappingConfig);

/*
 * Start the application
 */

var app = builder.Build();

app.Logger.LogInformation("[DC8-Framework] Starting the application!!!");

app.MapDefaultEndpoints();
app.UseSwagger();
app.UseSwaggerUI();
app.UseRequestLocalization(app.Services.GetService<IOptions<RequestLocalizationOptions>>()!.Value);
app.UseRateLimiter();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.AddMiddleware();
app.UseCors();
app.MapHub<NotificationHub>("/v1/notification-hub");
app.MapControllers();

app.Run();