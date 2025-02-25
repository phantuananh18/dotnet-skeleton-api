using FluentValidation;

namespace DotnetSkeleton.IdentityModule.Application.Commands.RefreshTokenCommand
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty().NotNull();
        }
    }
}