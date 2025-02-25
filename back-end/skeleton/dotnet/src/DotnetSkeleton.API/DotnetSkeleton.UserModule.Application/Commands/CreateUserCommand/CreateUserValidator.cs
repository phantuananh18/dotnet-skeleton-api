using FluentValidation;

namespace DotnetSkeleton.UserModule.Application.Commands.CreateUserCommand
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            //RuleFor(x => x.MobilePhone)
            //   .NotEmpty()
            //   .Matches(@"^\+\d{9,12}$")
            //   .WithMessage("Invalid phone number.");
        }
    }
}
