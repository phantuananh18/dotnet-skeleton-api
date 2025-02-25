using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DotnetSkeleton.SharedKernel.Utils.Models.Entities;

namespace DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities
{
    [Table(nameof(Feature))]
    public class Feature : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeatureId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}