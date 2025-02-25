using DotnetSkeleton.IdentityModule.Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetSkeleton.IdentityModule.Domain.Entities.MySQLEntities
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
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}