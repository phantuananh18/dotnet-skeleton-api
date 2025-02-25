using FluentValidation;

namespace DotnetSkeleton.IdentityModule.Application.Commands.OAuthCallBackCommand;

public class OAuthCallBackValidator : AbstractValidator<OAuthCallBackCommand>
{
    public OAuthCallBackValidator()
    {
        RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
    }
}