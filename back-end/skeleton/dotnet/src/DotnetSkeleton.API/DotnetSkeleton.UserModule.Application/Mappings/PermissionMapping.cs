using AutoMapper;
using DotnetSkeleton.UserModule.Application.Commands.CreatePermissionCommand;
using DotnetSkeleton.UserModule.Application.Commands.UpdatePermissionCommand;
using DotnetSkeleton.UserModule.Application.Queries.GetAllPermissionsQuery;
using DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.UserModule.Domain.Model.Requests.Permissions;
using DotnetSkeleton.UserModule.Domain.Model.Responses.PermissionResponses;

namespace DotnetSkeleton.UserModule.Application.Mappings
{
    public class PermissionMapping : Profile
    {
        public PermissionMapping()
        {
            CreateMap<Permission, PermissionResponse>()
                .ReverseMap();

            //Mapping Create role
            CreateMap<CreatePermissionCommand, CreatePermissionRequest>()
                .ReverseMap();

            CreateMap<CreatePermissionRequest, Permission>()
                .ReverseMap();

            //Mapping update role
            CreateMap<UpdatePermissionCommand, UpdatePermissionRequest>()
                .ReverseMap();

            CreateMap<UpdatePermissionRequest, Permission>()
                .ReverseMap();

            CreateMap<GetAllPermissionsQuery, GetAllPermissionsRequest>()
                .ReverseMap();
        }
    }
}