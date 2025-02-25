using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Queries.GetByIdRoleQuery
{
    public class GetByIdRoleHandler : IRequestHandler<GetByIdRoleQuery, BaseResponse>
    {

        private readonly IRoleService _userService;
        private readonly IMapper _mapper;

        public GetByIdRoleHandler(IRoleService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse> Handle(GetByIdRoleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _userService.GetRoleByRoleIdAsync(request.RoleId);
            }
            catch (Exception ex)
            {
                return BaseResponse.ServerError(ex.Message);
            }
        }
    }
}
