using FluentValidation;

namespace DotnetSkeleton.UserModule.Application.Commands.DeleteRolePermissionCommand
{
    public class DeleteRolePermissionValidator : AbstractValidator<DeleteRolePermissionCommand>
    {
        public DeleteRolePermissionValidator()
        {
            RuleFor(x => x.RolePermissionId)
                .Must(x => x > 0);
        }
    }
}
