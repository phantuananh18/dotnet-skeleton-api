namespace DotnetSkeleton.SharedKernel.Utils.Models.Responses;

public class ValidationError
{
    public List<FluentValidationError> ValidationErrors { get; set; } = new List<FluentValidationError>();
}

public class FluentValidationError
{
    public required string PropertyName { get; set; }
    public required string ErrorMessage { get; set; }
}