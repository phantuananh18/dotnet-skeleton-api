using AutoMapper;
using DotnetSkeleton.IdentityModule.Application.Commands.ForgotPasswordCommand;
using DotnetSkeleton.IdentityModule.Application.Commands.OAuthCallBackCommand;
using DotnetSkeleton.IdentityModule.Application.Commands.RefreshTokenCommand;
using DotnetSkeleton.IdentityModule.Application.Commands.SignInCommand;
using DotnetSkeleton.IdentityModule.Application.Commands.SignUpCommand;
using DotnetSkeleton.IdentityModule.Domain.Models.Requests.Auths;
using DotnetSkeleton.IdentityModule.Domain.Models.Requests.Users;

namespace DotnetSkeleton.IdentityModule.Application.Mappings
{
    /// <summary>
    /// Mapping for Auth
    /// </summary>
    public class MappingAuth : Profile
    {
        public MappingAuth()
        {
            CreateMap<SignUpCommand, SignUpRequest>()
                .ReverseMap();

            CreateMap<SignUpRequest, CreateUserRequest>()
                .ReverseMap();

            CreateMap<SignInCommand, SignInRequest>()
                .ReverseMap();

            CreateMap<RefreshTokenCommand, RefreshTokenRequest>()
                .ReverseMap();

            CreateMap<ForgotPasswordCommand, ForgotPasswordRequest>()
                .ReverseMap();

            CreateMap<OAuthCallBackCommand, OAuthCallBackRequest>()
                .ReverseMap();
        }
    }
}