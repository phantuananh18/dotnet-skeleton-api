using DotnetSkeleton.NotificationModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.NotificationModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.NotificationModule.Domain.Model.Requests.RecordNotification;
using DotnetSkeleton.NotificationModule.Infrastructure.DbContexts;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using MySql.Data.MySqlClient;

namespace DotnetSkeleton.NotificationModule.Infrastructure.Repositories.MySQL
{
    /// <summary>
    /// Represents a repository for managing <see cref="Notification"/> entities in the MySQL database.
    /// </summary>
    public class NotificationRepository : BaseRepository<Notification, long>, INotificationRepository
    {
        private readonly SkeletonDbContext _context;
        private readonly IOptionsMonitor<DatabaseOptions> _dbOptions;
        private readonly ILogger<NotificationRepository> _logger;

        public NotificationRepository(SkeletonDbContext context, IOptionsMonitor<DatabaseOptions> dbOptions, ILogger<NotificationRepository> logger) : base(context)
        {
            _context = context;
            _dbOptions = dbOptions;
            _logger = logger;
        }

        public async Task<int> RecordNotificationAsync(RecordNotificationRequest request)
        {
            var parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@TriggeredUserId", request.TriggeredUserId),
                new MySqlParameter("@NotificationTypeId", request.NotificationTypeId),
                new MySqlParameter("@Title", request.Title),
                new MySqlParameter("@Content", request.Content),
                new MySqlParameter("@SenderInfo", request.SenderInfo),
                new MySqlParameter("@CreatedBy", request.CreatedBy),
                new MySqlParameter("@FilterArg", request.FilterArg)
            };

            var result = await _context.Database.ExecuteSqlRawAsync("CALL RecordNotification(@TriggeredUserId, @NotificationTypeId, @Title, @Content, @SenderInfo, @CreatedBy, @FilterArg);", parameters);
            return result;
        }
    }
}