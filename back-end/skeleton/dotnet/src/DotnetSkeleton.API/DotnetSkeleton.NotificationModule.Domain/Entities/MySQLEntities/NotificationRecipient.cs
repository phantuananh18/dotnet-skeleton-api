using DotnetSkeleton.SharedKernel.Utils.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetSkeleton.NotificationModule.Domain.Entities.MySQLEntities
{
    public class NotificationRecipient : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NotificationRecipientId { get; set; }
        public int RecipientId { get; set; }
        public long NotificationId { get; set; }
        public bool IsRead { get; set; }
    }
}