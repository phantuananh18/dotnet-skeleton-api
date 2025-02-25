using DotnetSkeleton.SharedKernel.Utils.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities
{
    [Table(nameof(RolePermission))]
    public class RolePermission : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RolePermissionId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
    }
}
