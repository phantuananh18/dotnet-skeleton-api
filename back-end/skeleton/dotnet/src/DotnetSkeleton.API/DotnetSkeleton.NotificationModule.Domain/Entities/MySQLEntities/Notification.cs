using DotnetSkeleton.SharedKernel.Utils.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetSkeleton.NotificationModule.Domain.Entities.MySQLEntities
{
    /// <summary>
    /// Notification Entity
    /// </summary>
    [Table(nameof(Notification))]
    public class Notification : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required long NotificationId { get; set; }
        public required int NotificationTypeId { get; set; }
        public int? TriggeredUserId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? SenderInfo { get; set; }
    }
}