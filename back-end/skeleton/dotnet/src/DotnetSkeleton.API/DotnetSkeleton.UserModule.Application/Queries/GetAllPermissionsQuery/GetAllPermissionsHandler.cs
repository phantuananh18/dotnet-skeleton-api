using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using DotnetSkeleton.UserModule.Domain.Model.Requests.Permissions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DotnetSkeleton.UserModule.Application.Queries.GetAllPermissionsQuery
{
    public class GetAllPermissionsHandler : IRequestHandler<GetAllPermissionsQuery, BaseResponse>
    {
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPermissionsHandler> _logger;

        public GetAllPermissionsHandler(IPermissionService permissionService, IMapper mapper,
            ILogger<GetAllPermissionsHandler> logger)
        {
            _permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<BaseResponse> Handle(GetAllPermissionsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var request = _mapper.Map<GetAllPermissionsRequest>(query);
                return await _permissionService.GetAllPermissionsAsync(request);
            }
            catch (Exception ex)
            {
                string error = $"[GetAllPermissionsHandler] - {Helpers.BuildErrorMessage(ex)}";
                _logger.LogError(error);
                return BaseResponse.ServerError();
            }
        }
    }
}