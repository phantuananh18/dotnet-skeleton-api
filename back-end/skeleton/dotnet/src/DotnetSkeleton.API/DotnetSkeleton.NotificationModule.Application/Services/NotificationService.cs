using AutoMapper;
using DotnetSkeleton.NotificationModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.NotificationModule.Domain.Interfaces.Services;
using DotnetSkeleton.NotificationModule.Domain.Model.Requests.MessageInstance;
using DotnetSkeleton.NotificationModule.Domain.Model.Requests.NewFolder;
using DotnetSkeleton.NotificationModule.Domain.Model.Requests.RecordNotification;
using DotnetSkeleton.NotificationModule.Domain.Resources;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace DotnetSkeleton.NotificationModule.Application.Services
{
    /// <summary>
    /// Represents a service for managing notification entities.
    /// </summary>
    public class NotificationService : INotificationService
    {
        #region Fields
        // Repositories
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationTypeRepository _notificationTypeRepository;
        private readonly IUserRepository _userRepository;

        // Others
        private readonly IStringLocalizer<Resources> _localizer;
        private readonly ILogger<NotificationService> _logger;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignalROptions _signalROptions;

        #endregion

        #region Constructor
        public NotificationService(INotificationRepository notificationRepository,
            INotificationTypeRepository notificationTypeRepository,
            IUserRepository userRepository,
            IStringLocalizer<Resources> localizer,
            ILogger<NotificationService> logger,
            IMapper mapper,
            IHubContext<NotificationHub> hubContext,
            IHttpContextAccessor httpContextAccessor,
            IOptions<SignalROptions> signalROptions)
        {
            _notificationRepository = notificationRepository;
            _notificationTypeRepository = notificationTypeRepository;
            _userRepository = userRepository;
            _localizer = localizer;
            _logger = logger;
            _mapper = mapper;
            _hubContext = hubContext;
            _httpContextAccessor = httpContextAccessor;
            _signalROptions = signalROptions.Value;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Send Notification to all client.
        /// </summary>
        /// <param name="request">The detail of notification to send.</param>
        /// <returns>An action result representing the result of send notification</returns>
        public async Task<BaseResponse> SendNotificationAsync(SendNotificationRequest request)
        {
            var notificationContent = _mapper.Map<SendNotificationContent>(request);
            if (request.TriggeredUserId.HasValue)
            {
                var user = await _userRepository.FindByIdAsync(request.TriggeredUserId.Value);
                if (user == null)
                {
                    return BaseResponse.BadRequest(_localizer[nameof(Resources.User_Not_Found)]);
                }
                notificationContent.FirstName = user.FirstName;
                notificationContent.LastName = user.LastName;
            }

            var notificationType = await _notificationTypeRepository.FindOneAsync(x =>
                string.Equals(x.TypeName, request.NotificationType, StringComparison.CurrentCultureIgnoreCase));
            if (notificationType == null)
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.Notification_Type_Not_Found)]);
            }

            // Record notification
            var recordNotification = await RecordNotificationAsync(request, notificationType.NotificationTypeId);
            if (recordNotification < 0)
            {
                _logger.LogError($"[SendNotificationAsync] - Failed to record notification. Request: {JsonSerializer.Serialize(request)}");
                return BaseResponse.ServerError();
            }

            // Send notification to clients
            await _hubContext.Clients.All.SendAsync(_signalROptions.Channels.NotificationChannel, JsonSerializer.Serialize(notificationContent));
            return BaseResponse.Ok();
        }

        #endregion

        #region Private Methods
        private async Task<int> RecordNotificationAsync(SendNotificationRequest request, int notificationTypeId)
        {
            var notificationData = _mapper.Map<RecordNotificationRequest>(request);
            notificationData.NotificationTypeId = notificationTypeId;
            notificationData.CreatedBy = ((UserProfileData)_httpContextAccessor.HttpContext?.Items[Constant.FieldName.User]!)?.UserId ?? Constant.DatabaseAttribute.DefaultUser.UserId;
            notificationData.FilterArg = GenerateFilterArg(request);

            return await _notificationRepository.RecordNotificationAsync(notificationData);
        }

        private static string? GenerateFilterArg(SendNotificationRequest request)
        {
            switch (request.NotificationType)
            {
                case Constant.NotificationType.NewUser:
                    return Constant.NotificationFilter.NewUser;
                case Constant.NotificationType.NewEmail:
                    return string.Format(Constant.NotificationFilter.NewEmail, request.TriggeredUserId);
                default:
                    return null;
            }
        }
        #endregion
    }
}