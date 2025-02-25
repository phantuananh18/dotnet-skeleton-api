using AutoMapper;
using DotnetSkeleton.IdentityModule.Domain.Interfaces.Services;
using DotnetSkeleton.IdentityModule.Domain.Models.Requests.Auths;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.IdentityModule.Application.Commands.RefreshTokenCommand
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, BaseResponse>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public RefreshTokenHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<RefreshTokenRequest>(command);
            return await _authService.RefreshAccessToken(request);
        }
    }
}
