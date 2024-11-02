using CatalogAPI.Products.GetProducts;

namespace CatalogAPI.Products.GetProductsByCategory
{
    public record GetProductsByCategoryRequest(
        string Category,
        int? PageNumber = Constants.CommonQuery.DEFAULT_PAGE_NUMBER,
        int? PageSize = Constants.CommonQuery.DEFAULT_PAGE_SIZE
        );
    public record GetProductsByCategoryResponse(IEnumerable<Product> Products);

    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/product/category", async ([AsParameters] GetProductsByCategoryRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetProductsByCategoryQuery>();

                var result = await sender.Send(query);

                var response = result.Adapt<GetProductsByCategoryResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductsByCategory")
            .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products By Category")
            .WithDescription("Get Products By Category");
        }
    }
}
