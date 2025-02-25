using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using FluentValidation;
using MediatR;

namespace DotnetSkeleton.SharedKernel.Utils.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .Select(r => new FluentValidationError { PropertyName = r.PropertyName, ErrorMessage = r.ErrorMessage })
                .ToList();

            if (!failures.Any())
            {
                return await next();
            }

            var response = BaseResponse.BadRequest(null, new ValidationError { ValidationErrors = failures });
            return (TResponse)(object)response;
        }
    }
}
