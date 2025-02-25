using DotnetSkeleton.UserModule.Domain.Resources;
using FluentValidation;

namespace DotnetSkeleton.UserModule.Application.Queries.GetAllRolesQuery
{
    public class GetAllRolesValidator : AbstractValidator<GetAllRolesQuery>
    {
        public GetAllRolesValidator()
        {
            RuleFor(x => x.PageNumber).Must(x => x > 0)
                .WithMessage(x => string.Format(Resources.Invalid_Request, "PageNumber"));

            RuleFor(x => x.PageSize).Must(x => x > 0)
                .WithMessage(x => string.Format(Resources.Invalid_Request, "PageSize"));
        }
    }
}
