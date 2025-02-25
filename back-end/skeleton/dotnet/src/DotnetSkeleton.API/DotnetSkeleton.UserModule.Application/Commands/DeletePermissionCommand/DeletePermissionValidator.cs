using FluentValidation;

namespace DotnetSkeleton.UserModule.Application.Commands.DeletePermissionCommand
{
    public class DeletePermissionValidator : AbstractValidator<DeletePermissionCommand>
    {
        public DeletePermissionValidator()
        {
            RuleFor(x => x.PermissionId)
                .Must(x => x > 0);
        }
    }
}
