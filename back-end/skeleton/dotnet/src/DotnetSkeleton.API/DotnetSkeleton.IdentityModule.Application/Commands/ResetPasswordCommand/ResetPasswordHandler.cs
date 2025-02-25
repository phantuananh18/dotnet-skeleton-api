using DotnetSkeleton.IdentityModule.Domain.Interfaces.Services;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.IdentityModule.Application.Commands.ResetPasswordCommand;

public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, BaseResponse>
{
    private readonly IAuthService _authService;

    public ResetPasswordHandler(IAuthService authService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    public async Task<BaseResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        return await _authService.ResetPassword(request.Token, request.NewPassword);
    }
}