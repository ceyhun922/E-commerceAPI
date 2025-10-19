using BasetApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasetApi.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
            new Product { ProductID = 1, ProductName = "Product1", ProductPrice = 10,CategoryID=1 },
            new Product { ProductID = 2, ProductName = "Product2", ProductPrice = 20,CategoryID=2 },
            new Product { ProductID = 3, ProductName = "Product3", ProductPrice = 30,CategoryID=3 },
            new Product { ProductID = 4, ProductName = "Product4", ProductPrice = 20,CategoryID=2 },
            new Product { ProductID = 5, ProductName = "Product5", ProductPrice = 30,CategoryID=3 }
            );
        }
    }
}