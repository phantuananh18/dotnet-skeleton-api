namespace DotnetSkeleton.SharedKernel.Utils.Models.Options;

public class LocalizationOptions
{
    public static string JsonKey => nameof(LocalizationOptions);
    public required string DefaultCulture { get; set; }
    public required List<string> SupportedCultures { get; set; }
}