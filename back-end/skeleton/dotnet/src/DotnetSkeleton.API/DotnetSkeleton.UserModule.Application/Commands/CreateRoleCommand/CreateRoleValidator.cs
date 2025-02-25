using FluentValidation;

namespace DotnetSkeleton.UserModule.Application.Commands.CreateRoleCommand
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull();
        }
    }
}
