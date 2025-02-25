using DotnetSkeleton.SharedKernel.Utils.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetSkeleton.NotificationModule.Infrastructure.DbContexts.EntityTypeConfigurations
{
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            // Table name
            builder.ToTable(typeof(TEntity).Name);

            // Properties
            builder.Property(e => e.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}