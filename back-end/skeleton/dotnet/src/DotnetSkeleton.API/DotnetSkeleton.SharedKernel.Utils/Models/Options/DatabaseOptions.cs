namespace DotnetSkeleton.SharedKernel.Utils.Models.Options;

/// <summary>
/// Represents the options for the database connection.
/// </summary>
public class DatabaseOptions
{
    /// <summary>
    /// Gets the JSON key for the <see cref="DatabaseOptions"/> class.
    /// </summary>
    public static string JsonKey => nameof(DatabaseOptions);

    /// <summary>
    /// Gets or sets the connection string for the MySQL database. Omit if using MongoDB
    /// </summary>
    public required string MySQLConnectionString { get; set; }

    /// <summary>
    /// Gets or sets the connection string for the MongoDB database. Omit if using MySQL
    /// </summary>
    public required string MongoDBConnectionString { get; set; }
}