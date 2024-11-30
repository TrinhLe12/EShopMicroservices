using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects.StrongTypeIds;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.Id)
                .HasConversion(
                    orderItemId => orderItemId.Value,
                    dbId => OrderItemId.Of(dbId));

            //Each instance of OrderItem is associated with a single Product.
            builder.HasOne<Product>()
                //The relationship is one-to-many: One Product can appear in many OrderItem records
                .WithMany()
                //ProductId is the foreign key column in the OrderItem table that points to the primary key of the Product table
                .HasForeignKey(oi => oi.ProductId);

            builder.Property(oi => oi.Quantity).IsRequired();

            builder.Property(oi => oi.Price).IsRequired();
        }
    }
}
