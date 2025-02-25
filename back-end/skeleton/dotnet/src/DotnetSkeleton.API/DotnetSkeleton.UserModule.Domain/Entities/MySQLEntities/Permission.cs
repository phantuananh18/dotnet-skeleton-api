using DotnetSkeleton.SharedKernel.Utils.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities
{
    [Table(nameof(Permission))]
    public class Permission : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PermissionId { get; set; }

        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? Code { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public int FeatureId { get; set; }
    }
}
