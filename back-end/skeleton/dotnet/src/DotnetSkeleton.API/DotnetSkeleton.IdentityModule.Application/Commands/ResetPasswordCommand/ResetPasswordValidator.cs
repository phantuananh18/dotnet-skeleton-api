using FluentValidation;

namespace DotnetSkeleton.IdentityModule.Application.Commands.ResetPasswordCommand;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Token).NotNull().NotEmpty();
        RuleFor(x => x.NewPassword).NotNull().NotEmpty();
    }
}