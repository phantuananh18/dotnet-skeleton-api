using DotnetSkeleton.Core.Domain.Entities.BaseEntities;
using MongoDB.Bson.Serialization.Attributes;

namespace DotnetSkeleton.Core.Domain.Entities.MongoDb;

public class UserMongoDb : BaseMongoDbEntity
{
    [BsonElement("userId")]
    public int? UserId { get; set; }
}