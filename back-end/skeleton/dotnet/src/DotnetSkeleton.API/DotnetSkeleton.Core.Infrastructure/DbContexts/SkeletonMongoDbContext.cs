using DotnetSkeleton.Core.Domain.Entities.MongoDb;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace DotnetSkeleton.Core.Infrastructure.DbContexts;

public class SkeletonMongoDbContext : DbContext
{
    private readonly string _connectionString;
    private readonly ILogger<SkeletonMongoDbContext> _logger;

    public SkeletonMongoDbContext(DbContextOptions<SkeletonMongoDbContext> options, IOptionsMonitor<DatabaseOptions> dbOptions,
        ILogger<SkeletonMongoDbContext> logger) : base(options)
    {
        _logger = logger;
        _connectionString = dbOptions.CurrentValue.MongoDBConnectionString;
    }

    public virtual DbSet<UserMongoDb> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var clientSettings = MongoClientSettings.FromConnectionString(_connectionString);
        clientSettings.RetryWrites = true;
        clientSettings.ConnectTimeout = TimeSpan.FromSeconds(25);
        clientSettings.MaxConnectionLifeTime = TimeSpan.FromSeconds(60);

        optionsBuilder
            .UseMongoDB(new MongoClient(clientSettings), clientSettings.ApplicationName)
            .EnableSensitiveDataLogging()
            .UseLoggerFactory(LoggerFactory.Create(cfg => cfg.SetMinimumLevel(LogLevel.Information)))
            .LogTo(msg => _logger.LogInformation(msg), LogLevel.Information);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserMongoDb>(b =>
        {
            b.ToCollection("user");
        });
    }
}