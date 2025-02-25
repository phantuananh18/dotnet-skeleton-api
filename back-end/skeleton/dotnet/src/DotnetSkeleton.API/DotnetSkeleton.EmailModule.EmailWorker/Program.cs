using DotnetSkeleton.EmailModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.EmailModule.EmailWorker.Handler;
using DotnetSkeleton.EmailModule.EmailWorker.Services;
using DotnetSkeleton.EmailModule.EmailWorker.Services.Interfaces;
using DotnetSkeleton.EmailModule.Infrastructure.DbContexts;
using DotnetSkeleton.EmailModule.Infrastructure.Repositories;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using DotnetSkeleton.Utils.RabbitMqService;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection(Constant.SystemInfo.Logging));
builder.Logging.AddConsole();

// Read the configuration and configure the services
builder.Services.Configure<MailOptions>(builder.Configuration.GetSection(MailOptions.JsonKey));
builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection(RabbitMqOptions.JsonKey));
builder.Services.Configure<SystemInfoOptions>(builder.Configuration.GetSection(SystemInfoOptions.JsonKey));
builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.JsonKey));

// Register Client Service
// Default AddDbContext will use Scoped and WorkerService (AddHostedService) only allow Singleton
builder.Services.AddDbContext<SkeletonDbContext>(options => { }, ServiceLifetime.Singleton);
builder.Services.AddRabbitMqService();
builder.Services.AddSingleton<ICommunicationTemplateRepository, CommunicationTemplateRepository>();
builder.Services.AddSingleton<ICommunicationRepository, CommunicationRepository>();
builder.Services.AddSingleton<IEmailMetadataRepository, EmailMetadataRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IEmailQueueService, EmailQueueService>();

builder.Services.AddHttpContextAccessor();

// Register Worker
builder.Services.AddHostedService<OutgoingEmailWorker>();

var host = builder.Build();

host.Run();