using DotnetSkeleton.SharedKernel.Utils.Models.Responses;

namespace DotnetSkeleton.SharedKernel.Utils.Exceptions
{
    public class ValidationExceptionCustom : Exception
    {
        public IEnumerable<FluentValidationError> Errors { get; }

        public ValidationExceptionCustom()
            : base("One or more validation failures have occurred.")
        {
            Errors = new List<FluentValidationError>();
        }

        public ValidationExceptionCustom(IEnumerable<FluentValidationError> errors)
            : this()
        {
            Errors = errors;
        }
    }
}
