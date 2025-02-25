using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using DotnetSkeleton.UserModule.Domain.Model.Requests.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DotnetSkeleton.UserModule.Application.Queries.GetAllUsersQuery
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, BaseResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllUsersHandler> _logger;

        public GetAllUsersHandler(IUserService userService, IMapper mapper, ILogger<GetAllUsersHandler> logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<BaseResponse> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var request = _mapper.Map<GetAllUsersRequest>(query);
                return await _userService.GetAllUsersWithPaginationAsync(request);
            }
            catch (Exception ex)
            {
                string error = $"[GetAllUsersHandler] - {Helpers.BuildErrorMessage(ex)}";
                _logger.LogError(error);
                return BaseResponse.ServerError();
            }
        }
    }
}
