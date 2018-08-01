namespace Product_Shop.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using Models;
    
    public class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder.HasKey(e => new {e.CategoryId, e.ProductId});

            builder.HasOne(e => e.Category)
                .WithMany(e => e.CategoriesProducts)
                .HasForeignKey(e => e.CategoryId);

            builder.HasOne(e => e.Product)
                .WithMany(e => e.CategoriesProducts)
                .HasForeignKey(e => e.ProductId);
        }
    }
}