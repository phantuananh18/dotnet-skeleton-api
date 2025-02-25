using DotnetSkeleton.SharedKernel.Utils.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities
{
    /// <summary>
    /// Role entity
    /// </summary>

    [Table(nameof(Role))]
    public class Role : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
