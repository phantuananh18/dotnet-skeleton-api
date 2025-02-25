using FluentValidation;

namespace DotnetSkeleton.MessageModule.Application.Commands.SendSms
{
    public class SendSmsValidator : AbstractValidator<SendSmsCommand>
    {
        public SendSmsValidator()
        {
            RuleFor(x => x.Message)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.ToMobilePhone)
               .NotEmpty()
               .Matches(@"^\+\d{9,12}$")
               .WithMessage("Invalid phone number.");
        }
    }
}
