using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using DotnetSkeleton.UserModule.Domain.Model.Requests.RolePermissions;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.CreateRolePermissionCommand
{
    public class CreateRolePermissionHandler : IRequestHandler<CreateRolePermissionCommand, BaseResponse>
    {
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IMapper _mapper;

        public CreateRolePermissionHandler(IRolePermissionService rolePermissionService, IMapper mapper)
        {
            _rolePermissionService = rolePermissionService ?? throw new ArgumentNullException(nameof(rolePermissionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse> Handle(CreateRolePermissionCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var request = _mapper.Map<CreateRolePermissionRequest>(command);
                return await _rolePermissionService.CreateRolePermissionAsync(request);
            }
            catch (Exception ex)
            {
                return BaseResponse.ServerError(ex.Message);
            }
        }
    }
}
