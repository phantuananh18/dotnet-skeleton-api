using DotnetSkeleton.IdentityModule.Domain.Entities.MySQLEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetSkeleton.IdentityModule.Infrastructure.DbContexts.EntityTypeConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary key
            builder.HasKey(e => e.UserId);

            // Indexes
            builder.HasIndex(e => e.Username).IsUnique();
            builder.HasIndex(e => e.Email).IsUnique();
            builder.HasIndex(e => e.MobilePhone).IsUnique();

            // Properties
            builder.Property(e => e.UserId)
                .HasColumnName(nameof(User.UserId))
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Username)
                .HasColumnName(nameof(User.Username))
                .IsRequired();

            builder.Property(e => e.Password)
                .HasColumnName(nameof(User.Password))
                .IsRequired();

            builder.Property(e => e.Email)
                .HasColumnName(nameof(User.Email))
                .IsRequired();

            // Relationships
            builder.HasOne<Role>()
                .WithMany()
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}