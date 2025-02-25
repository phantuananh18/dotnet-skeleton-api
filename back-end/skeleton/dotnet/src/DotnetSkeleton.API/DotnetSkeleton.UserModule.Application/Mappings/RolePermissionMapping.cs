using AutoMapper;
using DotnetSkeleton.UserModule.Application.Commands.AssignRolePermissionsCommand;
using DotnetSkeleton.UserModule.Application.Commands.CreateRolePermissionCommand;
using DotnetSkeleton.UserModule.Application.Commands.UpdateRolePermissionCommand;
using DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.UserModule.Domain.Model.Requests.Permissions;
using DotnetSkeleton.UserModule.Domain.Model.Requests.RolePermissions;
using DotnetSkeleton.UserModule.Domain.Model.Responses.RolePermissionResponses;

namespace DotnetSkeleton.UserModule.Application.Mappings
{
    public class RolePermissionMapping : Profile
    {
        public RolePermissionMapping()
        {
            CreateMap<RolePermission, RolePermissionResponse>()
                .ReverseMap();

            //Mapping Create role
            CreateMap<CreateRolePermissionCommand, CreateRolePermissionRequest>()
                .ReverseMap();

            CreateMap<CreateRolePermissionRequest, RolePermission>()
                .ReverseMap();

            //Mapping update role
            CreateMap<UpdateRolePermissionCommand, UpdateRolePermissionRequest>()
                .ReverseMap();

            CreateMap<UpdateRolePermissionRequest, RolePermission>()
                .ReverseMap();

            // Mapping assign role permissions
            CreateMap<AssignRolePermissionsCommand, AssignRolePermissionRequest>()
                .ReverseMap();

            CreateMap<AssignPermissionRequest, RolePermission>()
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => !src.Allowed));
        }
    }
}
