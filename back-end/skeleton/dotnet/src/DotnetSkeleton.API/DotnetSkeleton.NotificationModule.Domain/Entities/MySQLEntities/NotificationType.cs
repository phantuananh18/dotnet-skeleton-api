using DotnetSkeleton.SharedKernel.Utils.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetSkeleton.NotificationModule.Domain.Entities.MySQLEntities
{
    /// <summary>
    /// NotificationType Entity
    /// </summary>
    [Table(nameof(NotificationType))]
    public class NotificationType : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int NotificationTypeId { get; set; }
        public required string TypeName { get; set; }
        public string? Description { get; set; }
    }
}