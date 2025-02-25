using AutoMapper;
using DotnetSkeleton.UserModule.Application.Commands.CreateRoleCommand;
using DotnetSkeleton.UserModule.Application.Commands.UpdateRoleCommand;
using DotnetSkeleton.UserModule.Application.Queries.GetAllRolesQuery;
using DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.UserModule.Domain.Model.Dtos.Roles;
using DotnetSkeleton.UserModule.Domain.Model.Requests.Roles;
using DotnetSkeleton.UserModule.Domain.Model.Responses.RoleResponses;

namespace DotnetSkeleton.UserModule.Application.Mappings
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<Role, RoleResponse>()
                .ReverseMap();

            //Maping Create role
            CreateMap<CreateRoleCommand, CreateRoleRequest>()
                .ReverseMap();

            CreateMap<CreateRoleRequest, Role>()
                .ReverseMap();

            //Mapping update role
            CreateMap<UpdateRoleCommand, UpdateRoleRequest>()
                .ReverseMap();

            CreateMap<UpdateRoleRequest, Role>()
                .ReverseMap();

            CreateMap<GetAllRolesQuery, GetAllRolesRequest>()
                .ReverseMap();

            CreateMap<RolePaginationDto, RolePaginationResponse>();
        }
    }
}
