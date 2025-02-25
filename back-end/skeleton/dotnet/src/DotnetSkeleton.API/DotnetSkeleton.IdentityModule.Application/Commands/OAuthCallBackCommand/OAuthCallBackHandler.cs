using AutoMapper;
using DotnetSkeleton.IdentityModule.Domain.Interfaces.Services;
using DotnetSkeleton.IdentityModule.Domain.Models.Requests.Auths;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.IdentityModule.Application.Commands.OAuthCallBackCommand;

public class OAuthCallBackHandler : IRequestHandler<OAuthCallBackCommand, BaseResponse>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public OAuthCallBackHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<BaseResponse> Handle(OAuthCallBackCommand command, CancellationToken cancellationToken)
    {
        var request = _mapper.Map<OAuthCallBackRequest>(command);
        return await _authService.OAuthCallBack(request);
    }
}