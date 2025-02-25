using AutoMapper;
using DotnetSkeleton.NotificationModule.Application.Commands.NotificationMessageCommand;
using DotnetSkeleton.NotificationModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.NotificationModule.Domain.Model.Requests.MessageInstance;
using DotnetSkeleton.NotificationModule.Domain.Model.Requests.NewFolder;
using DotnetSkeleton.NotificationModule.Domain.Model.Requests.RecordNotification;

namespace DotnetSkeleton.NotificationModule.Application.Mappings
{
    public class MappingNotification : Profile
    {
        public MappingNotification()
        {
            CreateMap<SendNotificationCommand, SendNotificationRequest>()
                .ReverseMap();

            CreateMap<SendNotificationRequest, Notification>()
                .ReverseMap();

            CreateMap<SendNotificationRequest, RecordNotificationRequest>()
                .ReverseMap();

            CreateMap<SendNotificationRequest, SendNotificationContent>()
                .ReverseMap();
        }
    }
}