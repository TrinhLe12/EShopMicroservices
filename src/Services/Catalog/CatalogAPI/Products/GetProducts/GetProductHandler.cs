namespace CatalogAPI.Products.GetProducts
{
    public record GetProductQuery(
        int? PageNumber = Constants.CommonQuery.DEFAULT_PAGE_NUMBER, 
        int? PageSize = Constants.CommonQuery.DEFAULT_PAGE_SIZE
        ) : IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> Products);

    internal class GetProductHandler (IDocumentSession session) 
        : IQueryHandler<GetProductQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                .ToPagedListAsync(
                    query.PageNumber ?? Constants.CommonQuery.DEFAULT_PAGE_NUMBER, 
                    query.PageSize ?? Constants.CommonQuery.DEFAULT_PAGE_SIZE, 
                    cancellationToken
                );

            return new GetProductResult(products);
        }
    }
}
