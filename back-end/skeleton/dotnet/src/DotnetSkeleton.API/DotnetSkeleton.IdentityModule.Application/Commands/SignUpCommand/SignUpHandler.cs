using AutoMapper;
using DotnetSkeleton.IdentityModule.Domain.Interfaces.Services;
using DotnetSkeleton.IdentityModule.Domain.Models.Requests.Auths;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.IdentityModule.Application.Commands.SignUpCommand
{
    public class SignUpHandler : IRequestHandler<SignUpCommand, BaseResponse>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public SignUpHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse> Handle(SignUpCommand command, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<SignUpRequest>(command);
            return await _authService.SignUp(request);
        }
    }
}