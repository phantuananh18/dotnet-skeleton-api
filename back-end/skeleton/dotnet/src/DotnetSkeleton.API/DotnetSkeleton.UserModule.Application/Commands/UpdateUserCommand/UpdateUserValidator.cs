using DotnetSkeleton.SharedKernel.Utils;
using FluentValidation;

namespace DotnetSkeleton.UserModule.Application.Commands.UpdateUserCommand
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.UserId)
                .Must(x => x > 0);
            //RuleFor(x => x.MobilePhone)
            //    .NotEmpty()
            //    .Matches(@"^\+\d{9,12}$")
            //    .WithMessage("Invalid phone number.");
            //RuleFor(x => x.Role)
            //    .NotEmpty()
            //    .NotNull()
            //    .Must(BeAllowedRole)
            //    .WithMessage("Role not be allowed");
        }

        //private bool BeAllowedRole(string? role)
        //{
        //    if (string.IsNullOrWhiteSpace(role))
        //    {
        //        return false;
        //    }

        //    return role is Constant.RoleType.Admin or Constant.RoleType.Client;
        //}
    }
}
