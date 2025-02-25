using DotnetSkeleton.UserModule.Domain.Resources;
using FluentValidation;

namespace DotnetSkeleton.UserModule.Application.Queries.GetAllPermissionsQuery
{
    public class GetAllPermissionsValidator : AbstractValidator<GetAllPermissionsQuery>
    {
        public GetAllPermissionsValidator()
        {
            RuleFor(x => x.PageNumber).Must(x => x > 0)
                .WithMessage(x => string.Format(Resources.Invalid_Request, "PageNumber"));

            RuleFor(x => x.PageSize).Must(x => x > 0)
                .WithMessage(x => string.Format(Resources.Invalid_Request, "PageSize"));

            RuleFor(x => x.RoleName).NotEmpty()
                .WithMessage(x => string.Format(Resources.Invalid_Request, "RoleName"));
        }
    }
}