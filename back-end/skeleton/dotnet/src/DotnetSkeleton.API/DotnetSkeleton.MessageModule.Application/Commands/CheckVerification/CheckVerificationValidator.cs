using FluentValidation;

namespace DotnetSkeleton.MessageModule.Application.Commands.CheckVerification
{
    public class CheckVerificationValidator : AbstractValidator<CheckVerificationCommand>
    {
        public CheckVerificationValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.MobilePhone)
               .NotEmpty()
               .Matches(@"^\+\d{9,12}$")
               .WithMessage("Invalid phone number.");
        }
    }
}
