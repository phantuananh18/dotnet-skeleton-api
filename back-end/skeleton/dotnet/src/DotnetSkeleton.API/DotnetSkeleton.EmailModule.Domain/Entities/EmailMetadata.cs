using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetSkeleton.EmailModule.Domain.Entities;

/// <summary>
/// EmailMetadata entity
/// </summary>
[Table(nameof(EmailMetadata))]
public class EmailMetadata : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EmailMetadataId { get; set; }
    public required int CommunicationId { get; set; }
    public int? CommunicationTemplateId { get; set; }
    public string? Subject { get; set; }
    public string? ContentUrl { get; set; }
    public string? ErrorMessage { get; set; }
}