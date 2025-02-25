using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Queries.GetByIdUserQuery
{
    public class GetByIdUserHandler : IRequestHandler<GetByIdUserQuery, BaseResponse>
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetByIdUserHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _userService.GetUserByUserIdAsync(request.UserId);
            }
            catch (Exception ex)
            {
                return BaseResponse.ServerError(ex.Message);
            }
        }
    }
}
