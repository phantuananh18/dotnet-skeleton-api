using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotnetSkeleton.Core.Domain.Entities.BaseEntities;

public class BaseMongoDbEntity
{
    [BsonElement("_id")]
    public ObjectId Id { get; set; }

    [BsonElement("createdBy")]
    public int? CreatedBy { get; set; }

    [BsonElement("updatedBy")]
    public int? UpdatedBy { get; set; }

    [BsonElement("createdDate")]
    public DateTime? CreatedDate { get; set; }

    [BsonElement("updatedDate")]
    public DateTime? UpdatedDate { get; set; }

    [BsonElement("isDeleted")]
    public bool? IsDeleted { get; set; } = false;
}