using FluentValidation;

namespace DotnetSkeleton.NotificationModule.Application.Commands.NotificationMessageCommand
{
    public class SendNotificationValidator : AbstractValidator<SendNotificationCommand>
    {
        public SendNotificationValidator()
        {
            RuleFor(x => x.NotificationType).NotEmpty().NotNull();
            RuleFor(x => x.Title).NotEmpty().NotNull();
            RuleFor(x => x.Content).NotEmpty().NotNull();
            RuleFor(x => x.SenderInfo).NotEmpty().NotNull();
        }
    }
}