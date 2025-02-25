using FluentValidation;

namespace DotnetSkeleton.UserModule.Application.Commands.CreatePermissionCommand
{
    public class CreatePermissionValidator : AbstractValidator<CreatePermissionCommand>
    {
        public CreatePermissionValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Code)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.FeatureId)
                .Must(x => x > 0);
        }
    }
}
