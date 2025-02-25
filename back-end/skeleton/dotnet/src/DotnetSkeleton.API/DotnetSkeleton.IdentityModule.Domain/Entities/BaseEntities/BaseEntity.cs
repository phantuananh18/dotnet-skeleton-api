namespace DotnetSkeleton.IdentityModule.Domain.Entities.BaseEntities
{
    public class BaseEntity
    {
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}