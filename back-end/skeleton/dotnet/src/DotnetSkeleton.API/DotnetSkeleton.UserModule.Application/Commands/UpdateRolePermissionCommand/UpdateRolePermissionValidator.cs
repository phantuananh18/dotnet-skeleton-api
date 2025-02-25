using FluentValidation;

namespace DotnetSkeleton.UserModule.Application.Commands.UpdateRolePermissionCommand
{
    public class UpdateRolePermissionValidator : AbstractValidator<UpdateRolePermissionCommand>
    {
        public UpdateRolePermissionValidator()
        {
            RuleFor(x => x.RolePermissionId)
                .Must(x => x > 0);

            RuleFor(x => x.RoleId)
                .Must(x => x > 0);

            RuleFor(x => x.PermissionId)
                .Must(x => x > 0);
        }
    }
}
