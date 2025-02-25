using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.DeletePermissionCommand
{
    public class DeletePermissionHandler : IRequestHandler<DeletePermissionCommand, BaseResponse>
    {
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;

        public DeletePermissionHandler(IPermissionService permissionService, IMapper mapper)
        {
            _permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse> Handle(DeletePermissionCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await _permissionService.DeletePermissionAsync(command.PermissionId);
            }
            catch (Exception ex)
            {
                return BaseResponse.ServerError(ex.Message);
            }
        }
    }
}
