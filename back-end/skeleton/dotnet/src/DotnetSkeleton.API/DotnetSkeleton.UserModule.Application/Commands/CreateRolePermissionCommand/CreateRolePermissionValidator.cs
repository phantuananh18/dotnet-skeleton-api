using FluentValidation;

namespace DotnetSkeleton.UserModule.Application.Commands.CreateRolePermissionCommand
{
    public class CreateRolePermissionValidator : AbstractValidator<CreateRolePermissionCommand>
    {
        public CreateRolePermissionValidator()
        {
            RuleFor(x => x.RoleId)
                .Must(x => x > 0);

            RuleFor(x => x.PermissionId)
                .Must(x => x > 0);
        }
    }
}
