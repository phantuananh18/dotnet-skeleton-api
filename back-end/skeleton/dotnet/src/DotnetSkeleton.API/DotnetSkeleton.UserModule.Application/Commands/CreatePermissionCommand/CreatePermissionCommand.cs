using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.CreatePermissionCommand
{
    public class CreatePermissionCommand : IRequest<BaseResponse>
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required string Description { get; set; }
        public required int FeatureId { get; set; }
    }
}