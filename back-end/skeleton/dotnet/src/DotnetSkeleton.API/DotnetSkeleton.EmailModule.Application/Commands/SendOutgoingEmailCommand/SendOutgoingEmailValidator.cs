using FluentValidation;

namespace DotnetSkeleton.EmailModule.Application.Commands.SendOutgoingEmailCommand;

public class SendOutgoingEmailValidator : AbstractValidator<SendOutgoingEmailCommand>
{
    public SendOutgoingEmailValidator()
    {
        RuleFor(x => x.EmailType).IsInEnum();
        RuleForEach(x => x.To).NotEmpty().NotNull().EmailAddress();
    }
}