using FluentValidation;

namespace DotnetSkeleton.UserModule.Application.Commands.DeleteRoleCommand
{
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleValidator()
        {
            RuleFor(x => x.RoleId)
                .Must(x => x > 0);
        }
    }
}
