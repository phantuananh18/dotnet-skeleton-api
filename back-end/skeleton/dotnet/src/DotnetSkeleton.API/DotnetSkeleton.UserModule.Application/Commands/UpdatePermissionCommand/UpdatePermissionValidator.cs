using FluentValidation;

namespace DotnetSkeleton.UserModule.Application.Commands.UpdatePermissionCommand
{
    public class UpdatePermissionValidator : AbstractValidator<UpdatePermissionCommand>
    {
        public UpdatePermissionValidator()
        {
            RuleFor(x => x.PermissionId)
                .Must(x => x > 0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Code)
               .NotEmpty()
               .NotNull();

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull();
        }
    }
}
