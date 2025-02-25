using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using DotnetSkeleton.UserModule.Domain.Model.Requests.Roles;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DotnetSkeleton.UserModule.Application.Queries.GetAllRolesQuery
{
    public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, BaseResponse>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllRolesHandler> _logger;

        public GetAllRolesHandler(IRoleService roleService, IMapper mapper, ILogger<GetAllRolesHandler> logger)
        {
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<BaseResponse> Handle(GetAllRolesQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var request = _mapper.Map<GetAllRolesRequest>(query);
                return await _roleService.GetAllRolesWithPaginationAsync(request);
            }
            catch (Exception ex)
            {
                string error = $"[GetAllRolesHandler] - {Helpers.BuildErrorMessage(ex)}";
                _logger.LogError(error);
                return BaseResponse.ServerError();
            }
        }
    }
}
