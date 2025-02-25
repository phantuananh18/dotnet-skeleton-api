using FluentValidation;

namespace DotnetSkeleton.IdentityModule.Application.Commands.ForgotPasswordCommand
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
        }
    }
}