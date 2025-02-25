namespace DotnetSkeleton.SharedKernel.Utils.Models.Requests;

public class PaginationBaseRequest
{
    /// <summary>
    /// Request the number of this page, default page number = 1
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Request the size of this page, default page size = 10
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Advanced filter string list. Each filter parameter will be separated by a comma.
    /// Filter format: filter=@filterColumn,@filterValueType,@filterOperator,@filterValue
    /// Example single filter: filter=firstName,string,contains,SampleName
    /// Example multiple filter: filter=firstName,string,contains,SampleName&filter=activeStatus,boolean,equal,false
    /// </summary>
    public string[]? Filter { get; set; }

    /// <summary>
    /// The condition between each filter option (optional). If we not use the advanced filter, leave it as null.
    /// Accepted value: "and" or "or"
    /// </summary>
    public string? FilterCondition { get; set; } = null;

    /// <summary>
    /// Advanced sort string list. Each sort parameter will be separated by a comma.
    /// Sort format: sort=@sortColumn,@sortDirection
    /// Example: sort=firstName,asc
    /// </summary>
    public string[]? Sort { get; set; }

    /// <summary>
    /// Search text value.
    /// </summary>
    public string? SearchText { get; set; }
}