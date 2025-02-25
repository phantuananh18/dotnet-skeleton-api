using FluentValidation;

namespace DotnetSkeleton.UserModule.Application.Commands.AssignRolePermissionsCommand
{
    public class AssignRolePermissionsValidator : AbstractValidator<AssignRolePermissionsCommand>
    {
        public AssignRolePermissionsValidator()
        {
            RuleFor(x => x.RoleId).Must(x => x > 0);

            RuleFor(x => x.Permissions).NotEmpty();
            
            RuleForEach(x => x.Permissions).ChildRules(p =>
            {
                p.RuleFor(r => r.PermissionId).Must(r => r > 0);
            });
        }
    }
}
