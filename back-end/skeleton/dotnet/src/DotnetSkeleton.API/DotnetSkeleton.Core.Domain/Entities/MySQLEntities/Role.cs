using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DotnetSkeleton.SharedKernel.Utils.Models.Entities;

namespace DotnetSkeleton.Core.Domain.Entities.MySQLEntities
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