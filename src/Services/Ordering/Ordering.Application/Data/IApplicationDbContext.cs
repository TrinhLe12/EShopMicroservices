namespace Ordering.Application.Data
{
    //This interface will be implemented in Infra layer --> Separate DB interaction logic from application layer
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; }
        DbSet<Product> Products { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderItem> OrderItems { get; }

        //This has no explicit implementation in ApplicationDbContext class in Infra layer
        //--> Will be inferred to the built-in SaveChangesAsync of EF
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
