using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetSkeleton.EmailModule.Domain.Entities;

/// <summary>
/// Communication entity
/// </summary>
[Table(nameof(Communication))]
public class Communication : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CommunicationId { get; set; }
    public required int SenderId { get; set; }
    public string? SenderInfo { get; set; }
    public int? ReceiverId { get; set; }
    public string? ReceiverInfo { get; set; }
    public required string CommunicationType { get; set; }
    public required string Direction { get; set; }
    public required DateTime CommunicationDatetime { get; set; }
    public required string Status { get; set; }
}