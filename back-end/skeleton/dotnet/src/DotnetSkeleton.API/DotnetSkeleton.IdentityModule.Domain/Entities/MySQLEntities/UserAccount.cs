using DotnetSkeleton.IdentityModule.Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetSkeleton.IdentityModule.Domain.Entities.MySQLEntities
{
    /// <summary>
    /// UserAccount entity
    /// </summary>
    [Table(nameof(UserAccount))]
    public class UserAccount : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserAccountId { get; set; }

        public int? UserId { get; set; }

        public string? AuthMethod { get; set; }

        [StringLength(50)]
        public string? OAuthProvider { get; set; }

        [StringLength(255)]
        public string? OAuthProviderAccountId { get; set; }

        [StringLength(50)]
        public string? SSOProvider { get; set; }

        [StringLength(255)]
        public string? SSOProviderUserId { get; set; }

        public bool TwoFactorEnabled { get; set; } = false;

        [StringLength(255)]
        public string? TwoFactorSecret { get; set; }

        [StringLength(255)]
        public string? TwoFactorBackupCodes { get; set; }
    }
}