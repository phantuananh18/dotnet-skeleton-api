﻿namespace DotnetSkeleton.UserModule.Domain.Model.Requests.Roles
{
    public class UpdateRoleRequest
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required string Description { get; set; }
    }
}
