using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using DotnetSkeleton.UserModule.Domain.Model.Requests.Permissions;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.UpdatePermissionCommand
{
    public class UpdatePermissionHandler : IRequestHandler<UpdatePermissionCommand, BaseResponse>
    {
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;

        public UpdatePermissionHandler(IPermissionService permissionService, IMapper mapper)
        {
            _permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse> Handle(UpdatePermissionCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var request = _mapper.Map<UpdatePermissionRequest>(command);
                return await _permissionService.UpdatedPermissionAsync(command.PermissionId, request);
            }
            catch (Exception ex)
            {
                return BaseResponse.ServerError(ex.Message);
            }
        }
    }
}
