using AutoMapper;
using DotnetSkeleton.UserModule.Application.Commands.CreateUserCommand;
using DotnetSkeleton.UserModule.Application.Commands.UpdateUserCommand;
using DotnetSkeleton.UserModule.Application.Queries.GetAllUsersQuery;
using DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.UserModule.Domain.Model.Dtos.Users;
using DotnetSkeleton.UserModule.Domain.Model.Requests.Users;
using DotnetSkeleton.UserModule.Domain.Model.Responses.UserResponses;

namespace DotnetSkeleton.UserModule.Application.Mappings
{
    /// <summary>
    /// Mapping for User
    /// </summary>
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserResponse>()
                .ReverseMap();

            CreateMap<CreateUserCommand, CreateUserRequest>()
                .ReverseMap();

            CreateMap<CreateUserRequest, User>()
               .ReverseMap();

            CreateMap<UserAccount, UserAccountResponse>()
                .ReverseMap();

            CreateMap<UpdateUserCommand, UpdateUserRequest>()
                .ReverseMap();

            CreateMap<GetAllUsersQuery, GetAllUsersRequest>()
                .ReverseMap();

            CreateMap<UserPaginationDto, UserPaginationResponse>();

            CreateMap<UserAccountRequest, UserAccount>()
                .ReverseMap();
        }
    }
}
