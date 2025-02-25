using DotnetSkeleton.UserModule.Domain.Resources;
using FluentValidation;

namespace DotnetSkeleton.UserModule.Application.Queries.GetAllUsersQuery
{
    public class GetAllUsersValidator : AbstractValidator<GetAllUsersQuery>
    {
        public GetAllUsersValidator()
        {
            RuleFor(x => x.PageNumber).Must(x => x > 0)
                .WithMessage(x => string.Format(Resources.Invalid_Request, "PageNumber"));

            RuleFor(x => x.PageSize).Must(x => x > 0)
                .WithMessage(x => string.Format(Resources.Invalid_Request, "PageSize"));
        }
    }
}
