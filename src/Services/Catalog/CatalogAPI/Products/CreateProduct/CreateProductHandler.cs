using Marten.Internal.Sessions;

namespace CatalogAPI.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    /// <summary>
    /// Marten acts like an abstract repository --> inject directly to handler, no need to wrap in another abtract repository layer
    /// </summary>
    /// <param name="session"></param>
    internal class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //Create product entity from command object
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
            };

            //Save to datatbase
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            //Return CreateProductResult result
            return new CreateProductResult(product.Id);
        }
    }
}
