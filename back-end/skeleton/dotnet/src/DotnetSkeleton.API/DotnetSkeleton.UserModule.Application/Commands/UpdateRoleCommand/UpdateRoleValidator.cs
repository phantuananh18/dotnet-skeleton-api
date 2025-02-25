using FluentValidation;

namespace DotnetSkeleton.UserModule.Application.Commands.UpdateRoleCommand
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleValidator()
        {
            RuleFor(x => x.RoleId)
                .Must(x => x > 0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull();
        }
    }
}
