using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetSkeleton.EmailModule.Domain.Entities;

/// <summary>
/// CommunicationTemplate entity
/// </summary>
[Table(nameof(CommunicationTemplate))]
public class CommunicationTemplate : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CommunicationTemplateId { get; set; }
    public required string TemplateName { get; set; }
    public required string Subject { get; set; }
    public required string TemplateContentUrl { get; set; }
}