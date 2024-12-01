namespace BuildingBlocks.Pagination
{
    /// <summary>
    /// Common PaginationRequest
    /// </summary>
    /// <param name="PageIndex"></param>
    /// <param name="PageSize"></param>
    public record PaginationRequest(int PageIndex = 0, int PageSize = 10);
}
