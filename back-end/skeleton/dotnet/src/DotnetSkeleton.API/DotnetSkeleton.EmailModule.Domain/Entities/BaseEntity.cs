namespace DotnetSkeleton.EmailModule.Domain.Entities;

/// <summary>
/// Base Entity's Columns
/// </summary>
public class BaseEntity
{
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool IsDeleted { get; set; } = false;
}