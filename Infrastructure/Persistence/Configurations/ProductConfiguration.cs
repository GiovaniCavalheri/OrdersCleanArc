using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(prop => prop.ProductName).IsRequired().HasMaxLength(100);
            builder.Property(prop => prop.ProductPrice).IsRequired().HasColumnType("decimal(18,2)");
        }
    }
}
