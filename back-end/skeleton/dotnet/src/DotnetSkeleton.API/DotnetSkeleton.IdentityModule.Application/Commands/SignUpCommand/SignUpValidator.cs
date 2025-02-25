using DotnetSkeleton.SharedKernel.Utils;
using FluentValidation;

namespace DotnetSkeleton.IdentityModule.Application.Commands.SignUpCommand
{
    public class SignUpValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.MobilePhone)
                .NotEmpty()
                .Matches(Constant.Regex.PhoneNumberRegex)
                .WithMessage("Invalid phone number.");
        }
    }
}