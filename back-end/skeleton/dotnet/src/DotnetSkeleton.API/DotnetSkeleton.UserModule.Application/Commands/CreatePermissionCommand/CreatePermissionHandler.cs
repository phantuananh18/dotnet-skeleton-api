using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using DotnetSkeleton.UserModule.Domain.Model.Requests.Permissions;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.CreatePermissionCommand
{
    public class CreatePermissionHandler : IRequestHandler<CreatePermissionCommand, BaseResponse>
    {
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;

        public CreatePermissionHandler(IPermissionService permissionService, IMapper mapper)
        {
            _permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse> Handle(CreatePermissionCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var request = _mapper.Map<CreatePermissionRequest>(command);
                return await _permissionService.CreatePermissionAsync(request);
            }
            catch (Exception ex)
            {
                return BaseResponse.ServerError(ex.Message);
            }
        }
    }
}
