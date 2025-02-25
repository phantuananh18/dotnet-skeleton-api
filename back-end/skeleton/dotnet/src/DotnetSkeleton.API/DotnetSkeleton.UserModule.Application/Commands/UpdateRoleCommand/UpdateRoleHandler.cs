using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using DotnetSkeleton.UserModule.Domain.Model.Requests.Roles;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.UpdateRoleCommand
{
    public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, BaseResponse>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UpdateRoleHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var request = _mapper.Map<UpdateRoleRequest>(command);
                return await _roleService.UpdatedRoleAsync(command.RoleId, request);
            }
            catch (Exception ex)
            {
                return BaseResponse.ServerError(ex.Message);
            }
        }
    }
}
