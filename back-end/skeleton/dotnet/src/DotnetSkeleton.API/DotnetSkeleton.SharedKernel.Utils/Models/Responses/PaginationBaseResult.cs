namespace DotnetSkeleton.SharedKernel.Utils.Models.Responses;

public class PaginationBaseResult<T> where T : class
{
    /// <summary>
    /// The page number this page represents.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// The size of this page.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// The total number of pages available.
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// The total number of records available.
    /// </summary>
    public int TotalNumberOfRecords { get; set; }

    /// <summary>
    /// Is exist the previous page
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>
    /// Is exist the next page
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// The records this page represents.
    /// </summary>
    public T? Results { get; set; }
}