using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.DeleteRoleCommand
{
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, BaseResponse>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public DeleteRoleHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await _roleService.DeleteRoleAsync(command.RoleId);
            }
            catch (Exception ex)
            {
                return BaseResponse.ServerError(ex.Message);
            }
        }
    }
}