using DotnetSkeleton.SharedKernel.Utils.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetSkeleton.NotificationModule.Domain.Entities.MySQLEntities
{
    public class UserSetting : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SettingId { get; set; }
        public int UserId { get; set; }
        public bool IsNotificationEnable { get; set; }
    }
}