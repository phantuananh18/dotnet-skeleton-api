using AutoMapper;
using DotnetSkeleton.IdentityModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.IdentityModule.Domain.Models.Dto;
using DotnetSkeleton.IdentityModule.Domain.Models.Requests.Auths;
using DotnetSkeleton.IdentityModule.Domain.Models.Requests.Users;
using DotnetSkeleton.IdentityModule.Domain.Models.Responses.SignInResponse;
using DotnetSkeleton.IdentityModule.Domain.Models.Responses.UserResponse;

namespace DotnetSkeleton.IdentityModule.Application.Mappings
{
    public class MappingUser : Profile
    {
        public MappingUser()
        {
            CreateMap<User, UserResponse>()
                .ReverseMap();

            CreateMap<CreateUserRequest, User>()
                .ReverseMap();

            CreateMap<UserAccount, UserAccountResponse>()
                .ReverseMap();

            CreateMap<UserResponse, UserInformation>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role!.RoleName));

            CreateMap<UserAndRelatedData, UserInformation>()
                .ForPath(dest => dest.Role, opt => opt.MapFrom(src => src.Role!.Name));

            CreateMap<CreateUserPayload, OAuthCallBackRequest>()
                .ReverseMap();

            CreateMap<UpdateUserPayload, OAuthCallBackRequest>()
                .ReverseMap();

            CreateMap<UserAccountRequest, OAuthCallBackRequest>()
                .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src.OAuthProvider))
                .ForMember(dest => dest.ProviderAccountId, opt => opt.MapFrom(src => src.OAuthProviderAccountId))
                .ReverseMap();
        }
    }
}