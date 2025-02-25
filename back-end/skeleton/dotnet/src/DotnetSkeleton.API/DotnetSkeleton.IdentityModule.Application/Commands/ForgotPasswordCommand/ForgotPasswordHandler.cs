using AutoMapper;
using DotnetSkeleton.IdentityModule.Domain.Interfaces.Services;
using DotnetSkeleton.IdentityModule.Domain.Models.Requests.Auths;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.IdentityModule.Application.Commands.ForgotPasswordCommand
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, BaseResponse>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public ForgotPasswordHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var request = _mapper.Map<ForgotPasswordRequest>(command);
                return await _authService.ForgotPassword(request);
            }
            catch (Exception ex)
            {
                return BaseResponse.ServerError(ex.Message);
            }
        }
    }
}