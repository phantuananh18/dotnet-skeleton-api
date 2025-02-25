using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using DotnetSkeleton.UserModule.Domain.Model.Requests.RolePermissions;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.UpdateRolePermissionCommand
{
    public class UpdateRolePermissionHandler : IRequestHandler<UpdateRolePermissionCommand, BaseResponse>
    {
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IMapper _mapper;

        public UpdateRolePermissionHandler(IRolePermissionService rolePermissionService, IMapper mapper)
        {
            _rolePermissionService = rolePermissionService ?? throw new ArgumentNullException(nameof(rolePermissionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse> Handle(UpdateRolePermissionCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var request = _mapper.Map<UpdateRolePermissionRequest>(command);
                return await _rolePermissionService.UpdatedRolePermissionAsync(command.RolePermissionId, request);
            }
            catch (Exception ex)
            {
                return BaseResponse.ServerError(ex.Message);
            }
        }
    }
}
