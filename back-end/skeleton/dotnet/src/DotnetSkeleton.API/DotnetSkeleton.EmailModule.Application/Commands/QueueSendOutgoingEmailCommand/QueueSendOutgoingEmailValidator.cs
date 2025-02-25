using FluentValidation;

namespace DotnetSkeleton.EmailModule.Application.Commands.QueueSendOutgoingEmailCommand;

public class QueueSendOutgoingEmailValidator : AbstractValidator<QueueSendOutgoingEmailCommand>
{
    public QueueSendOutgoingEmailValidator()
    {
        RuleFor(x => x.EmailType).IsInEnum();
        RuleForEach(x => x.To).NotEmpty().NotNull().EmailAddress();
    }
}