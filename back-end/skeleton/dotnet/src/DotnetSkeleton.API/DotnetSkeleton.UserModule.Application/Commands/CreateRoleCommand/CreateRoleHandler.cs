using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using DotnetSkeleton.UserModule.Domain.Model.Requests.Roles;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.CreateRoleCommand
{
    public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, BaseResponse>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public CreateRoleHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var request = _mapper.Map<CreateRoleRequest>(command);
                return await _roleService.CreateRoleAsync(request);
            }
            catch (Exception ex)
            {
                return BaseResponse.ServerError(ex.Message);
            }
        }
    }
}
