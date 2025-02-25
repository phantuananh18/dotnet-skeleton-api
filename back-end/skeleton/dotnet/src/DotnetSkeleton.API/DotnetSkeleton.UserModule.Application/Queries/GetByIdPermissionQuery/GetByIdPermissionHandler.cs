using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Queries.GetByIdPermissionQuery
{
    public class GetByIdPermissionHandler : IRequestHandler<GetByIdPermissionQuery, BaseResponse>
    {

        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;

        public GetByIdPermissionHandler(IPermissionService permissionService, IMapper mapper)
        {
            _permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse> Handle(GetByIdPermissionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _permissionService.GetPermissionByPermissionIdAsync(request.PermissionId);
            }
            catch (Exception ex)
            {
                return BaseResponse.ServerError(ex.Message);
            }
        }
    }
}
