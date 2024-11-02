namespace CatalogAPI.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(
        string Category,
        int? PageNumber = Constants.CommonQuery.DEFAULT_PAGE_NUMBER,
        int? PageSize = Constants.CommonQuery.DEFAULT_PAGE_SIZE
        ) : IQuery<GetProductsBtCategoryResult>;
    public record GetProductsBtCategoryResult(IEnumerable<Product> Products);

    internal class GetProductsByCategoryHandler(IDocumentSession session)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsBtCategoryResult>
    {
        public async Task<GetProductsBtCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                .Where(p => p.Category.Contains(query.Category))
                .ToPagedListAsync(
                    query.PageNumber ?? Constants.CommonQuery.DEFAULT_PAGE_NUMBER,
                    query.PageSize ?? Constants.CommonQuery.DEFAULT_PAGE_SIZE,
                    cancellationToken
                );

            return new GetProductsBtCategoryResult(products);
        }
    }
}
