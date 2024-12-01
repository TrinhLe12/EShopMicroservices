namespace BuildingBlocks.Pagination
{
    /// <summary>
    /// Common PaginatedResult wrapper
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="count"></param>
    /// <param name="data"></param>
    public class PaginatedResult<TEntity> 
        (int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
        where TEntity : class
    {
        public int PageIndex { get; } = pageIndex;
        public int PageSize { get; } = pageSize;
        public long Count { get; } = count;
        public IEnumerable<TEntity> Data { get; } = data;
    }
}
