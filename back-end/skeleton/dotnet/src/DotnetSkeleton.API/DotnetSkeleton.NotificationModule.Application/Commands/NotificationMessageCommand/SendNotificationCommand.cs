using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.NotificationModule.Application.Commands.NotificationMessageCommand
{
    public class SendNotificationCommand : IRequest<BaseResponse>
    {
        public int? TriggeredUserId { get; set; }
        public required string NotificationType { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string SenderInfo { get; set; }
    }
}