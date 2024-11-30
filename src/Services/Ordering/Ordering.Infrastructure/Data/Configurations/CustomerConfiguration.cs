using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects.StrongTypeIds;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            //Set column "Id" as PK in table
            builder.HasKey(c => c.Id);

            //Declare column "Id" in table
            builder.Property(c => c.Id)
                //Convert strong type Id to column Id and vice versa
                .HasConversion(
                    customerId => customerId.Value,
                    dbId => CustomerId.Of(dbId));

            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();

            builder.Property(c => c.Email).HasMaxLength(255);

            builder.HasIndex(c => c.Email).IsUnique();
        }
    }
}
