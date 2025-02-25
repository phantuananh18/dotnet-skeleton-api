using AutoMapper;
using DotnetSkeleton.IdentityModule.Domain.Interfaces.Services;
using DotnetSkeleton.IdentityModule.Domain.Models.Requests.Auths;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.IdentityModule.Application.Commands.SignInCommand
{
    public class SignInHandler : IRequestHandler<SignInCommand, BaseResponse>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public SignInHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse> Handle(SignInCommand command, CancellationToken cancellationToken)
        {
            var signInRequest = _mapper.Map<SignInRequest>(command);
            return await _authService.SignIn(signInRequest);
        }
    }
}
