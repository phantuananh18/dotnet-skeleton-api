using DotnetSkeleton.MessageModule.Application.Commands.StartVerification;
using FluentValidation;

namespace DotnetSkeleton.MessageModule.Application.Commands.CreateVerification
{
    public class StartVerificationValidator : AbstractValidator<StartVerificationCommand>
    {
        public StartVerificationValidator()
        {
            RuleFor(x => x.Chanel)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.MobilePhone)
               .NotEmpty()
               .Matches(@"^\+\d{9,12}$")
               .WithMessage("Invalid phone number.");
        }
    }
}
