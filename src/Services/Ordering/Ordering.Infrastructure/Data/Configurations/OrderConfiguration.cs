using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasConversion(
                    orderId => orderId.Value,
                    dbId => OrderId.Of(dbId));

            //Which entity defines the rules for how its children should be created,
            //the builder should be part of that entity
            builder.HasMany(o => o.OrderItems) //Specify navigation property --> allow navigating to related data
                .WithOne()
                .HasForeignKey(oi => oi.OrderId)
                .IsRequired();

            builder.HasOne<Customer>() //Navigation property not specified --> Relation ship still created in DB, but can't navigate to Customer in c# model
                .WithMany()
                .HasForeignKey(o => o.CustomerId);

            //Define complex property represent value object of DDD
            builder.ComplexProperty(
               o => o.OrderName, nameBuilder =>
               {
                    nameBuilder.Property(n => n.Value)
                        .HasColumnName(nameof(Order.OrderName))
                        .HasMaxLength(100)
                        .IsRequired();
               });

            builder.ComplexProperty(
               o => o.ShippingAddress, addressBuilder =>
               {
                   //If not specify column name --> ShippingAddress_FirstName
                   addressBuilder.Property(a => a.FirstName)
                       .HasMaxLength(50)
                       .IsRequired();

                   addressBuilder.Property(a => a.LastName)
                        .HasMaxLength(50)
                        .IsRequired();

                   addressBuilder.Property(a => a.EmailAddress)
                       .HasMaxLength(50);

                   addressBuilder.Property(a => a.AddressLine)
                       .HasMaxLength(180)
                       .IsRequired();

                   addressBuilder.Property(a => a.Country)
                       .HasMaxLength(50);

                   addressBuilder.Property(a => a.State)
                       .HasMaxLength(50);

                   addressBuilder.Property(a => a.ZipCode)
                       .HasMaxLength(5)
                       .IsRequired();
               });

            builder.ComplexProperty(
                o => o.BillingAddress, addressBuilder =>
                {
                    addressBuilder.Property(a => a.FirstName)
                        .HasMaxLength(50)
                        .IsRequired();

                    addressBuilder.Property(a => a.LastName)
                        .HasMaxLength(50)
                        .IsRequired();

                    addressBuilder.Property(a => a.EmailAddress)
                        .HasMaxLength(50);

                    addressBuilder.Property(a => a.AddressLine)
                        .HasMaxLength(180)
                        .IsRequired();

                    addressBuilder.Property(a => a.Country)
                        .HasMaxLength(50);

                    addressBuilder.Property(a => a.State)
                        .HasMaxLength(50);

                    addressBuilder.Property(a => a.ZipCode)
                        .HasMaxLength(5)
                        .IsRequired();
                });

            builder.ComplexProperty(
                o => o.Payment, paymentBuilder => 
                { 
                    paymentBuilder.Property(p => p.CardName)
                        .HasMaxLength(50)
                        .IsRequired();

                    paymentBuilder.Property(p => p.CardNumber)
                        .HasMaxLength(24)
                        .IsRequired();

                    paymentBuilder.Property(p => p.Expiration)
                        .HasMaxLength(10);

                    paymentBuilder.Property(p => p.CVV)
                        .HasMaxLength(3);

                    paymentBuilder.Property(p => p.PaymentMethod);
                });

            builder.Property(o => o.Status)
                .HasConversion(
                    status => status.ToString(),
                    dbStatus => Enum.Parse<OrderStatus>(dbStatus)
                );

            builder.Property(o => o.TotalPrice);
        }
    }
}
