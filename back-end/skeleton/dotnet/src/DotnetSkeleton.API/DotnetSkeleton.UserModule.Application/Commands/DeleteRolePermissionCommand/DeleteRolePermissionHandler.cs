using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.DeleteRolePermissionCommand
{
    internal class DeleteRolePermissionHandler : IRequestHandler<DeleteRolePermissionCommand, BaseResponse>
    {
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IMapper _mapper;

        public DeleteRolePermissionHandler(IRolePermissionService rolePermissionService, IMapper mapper)
        {
            _rolePermissionService = rolePermissionService ?? throw new ArgumentNullException(nameof(rolePermissionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse> Handle(DeleteRolePermissionCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await _rolePermissionService.DeleteRolePermissionAsync(command.RolePermissionId);
            }
            catch (Exception ex)
            {
                return BaseResponse.ServerError(ex.Message);
            }
        }
    }
}
