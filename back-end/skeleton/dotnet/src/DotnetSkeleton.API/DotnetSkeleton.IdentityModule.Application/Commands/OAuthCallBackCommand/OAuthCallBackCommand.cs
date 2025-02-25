using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.IdentityModule.Application.Commands.OAuthCallBackCommand;

public class OAuthCallBackCommand : IRequest<BaseResponse>
{
    public required string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public string? MobilePhone { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? Provider { get; set; }
    public string? ProviderAccountId { get; set; }
    public string? Role { get; set; } = Constant.RoleType.Client;
}