using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Model.Requests.Users;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.UpdateUserCommand
{
    public class UpdateUserCommand : IRequest<BaseResponse>
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MobilePhone { get; set; }
        public string? Role { get; set; }
        public UserAccountRequest? UserAccount { get; set; }
    }
}
