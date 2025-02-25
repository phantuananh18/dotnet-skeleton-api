using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using DotnetSkeleton.UserModule.Domain.Model.Requests.RolePermissions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DotnetSkeleton.UserModule.Application.Commands.AssignRolePermissionsCommand
{
    public class AssignRolePermissionsHandler : IRequestHandler<AssignRolePermissionsCommand, BaseResponse>
    {
        private readonly ILogger<AssignRolePermissionsHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRolePermissionService _rolePermissionService;

        public AssignRolePermissionsHandler(ILogger<AssignRolePermissionsHandler> logger, IMapper mapper, IRolePermissionService rolePermissionService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _rolePermissionService = rolePermissionService ?? throw new ArgumentNullException(nameof(rolePermissionService));
        }

        public async Task<BaseResponse> Handle(AssignRolePermissionsCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var request = _mapper.Map<AssignRolePermissionRequest>(command);
                return await _rolePermissionService.AssignRolePermissionsAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[AssignRolePermissionHandler] - {Helpers.BuildErrorMessage(ex)}");
                return BaseResponse.ServerError();
            }
        }
    }
}
