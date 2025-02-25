using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models;
using DotnetSkeleton.SharedKernel.Utils.Models.Entities;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities;

namespace DotnetSkeleton.UserModule.Infrastructure.DbContexts
{
    public class SkeletonDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<SkeletonDbContext> _logger;

        public SkeletonDbContext(DbContextOptions<SkeletonDbContext> options, IHttpContextAccessor httpContextAccessor,
            ILogger<SkeletonDbContext> logger, IOptionsMonitor<DatabaseOptions> dbOptions) : base(options)
        {
            _connectionString = dbOptions.CurrentValue.MySQLConnectionString;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<Feature> Features { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySQL(_connectionString,
                    builder =>
                    {
                        builder.EnableRetryOnFailure(3, TimeSpan.FromSeconds(10), null);
                        builder.CommandTimeout(25);
                        builder.MaxBatchSize(100);
                    })
                .UseLoggerFactory(LoggerFactory.Create(cfg => cfg.SetMinimumLevel(LogLevel.Information)))
                .LogTo(msg => _logger.LogInformation(msg), LogLevel.Information)
                .EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var currentUser = (UserProfileData)_httpContextAccessor.HttpContext?.Items[Constant.FieldName.User]!;
            var entries = ChangeTracker.Entries().Where(e => e.State is EntityState.Added or EntityState.Modified);
            var utcNow = DateTime.UtcNow;
            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity entity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entity.CreatedBy = currentUser?.UserId ?? Constant.DatabaseAttribute.DefaultUser.UserId;
                            entity.CreatedDate = utcNow;
                            entity.IsDeleted = false;
                            break;

                        case EntityState.Modified:
                            entity.UpdatedBy = currentUser?.UserId ?? Constant.DatabaseAttribute.DefaultUser.UserId;
                            entity.UpdatedDate = utcNow;
                            break;
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
