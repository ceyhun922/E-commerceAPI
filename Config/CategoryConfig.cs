using BasetApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasetApi.Config
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
            new Category { CategoryID = 1, CategoryName = "Category1", CategoryStatus = true },
            new Category { CategoryID = 2, CategoryName = "Category2", CategoryStatus = true },
            new Category { CategoryID = 3, CategoryName = "Category3", CategoryStatus = true }
            );
        }
    }
}