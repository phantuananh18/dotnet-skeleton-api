namespace DotnetSkeleton.SharedKernel.Utils.Models.Options;

public class EncryptOptions
{
    public static string JsonKey => nameof(EncryptOptions);
    public int BcryptSaltRound { get; set; }
}