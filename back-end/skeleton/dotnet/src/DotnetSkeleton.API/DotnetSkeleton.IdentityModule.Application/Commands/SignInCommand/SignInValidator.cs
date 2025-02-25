using FluentValidation;

namespace DotnetSkeleton.IdentityModule.Application.Commands.SignInCommand
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        public SignInValidator()
        {
            RuleFor(x => x.Username).NotEmpty().NotNull();
            RuleFor(x => x.Password).NotEmpty().NotNull();
        }
    }
}