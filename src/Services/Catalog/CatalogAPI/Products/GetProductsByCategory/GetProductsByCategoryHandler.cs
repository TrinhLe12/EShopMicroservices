
using CatalogAPI.Products.GetProducts;
using Microsoft.Extensions.Logging;

namespace CatalogAPI.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsBtCategoryResult>;
    public record GetProductsBtCategoryResult(IEnumerable<Product> Products);

    internal class GetProductsByCategoryHandler(IDocumentSession session, ILogger<GetProductHandler> logger)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsBtCategoryResult>
    {
        public async Task<GetProductsBtCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsByCategoryHandler.Handle called with {@query}", query);

            var products = await session.Query<Product>()
                .Where(p => p.Category.Contains(query.Category))
                .ToListAsync(cancellationToken);

            return new GetProductsBtCategoryResult(products);
        }
    }
}
