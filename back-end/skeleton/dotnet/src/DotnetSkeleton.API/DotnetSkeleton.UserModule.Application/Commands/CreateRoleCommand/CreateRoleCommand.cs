using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.CreateRoleCommand
{
    public class CreateRoleCommand : IRequest<BaseResponse>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
