namespace Product_Shop.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using Models;

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode();

            builder.Property(e => e.Price)
                .IsRequired();
            
            builder.HasOne(e => e.Buyer)
                .WithMany(e => e.BoughtProducts)
                .HasForeignKey(e => e.BuyerId);

            builder.HasOne(e => e.Seller)
                .WithMany(e => e.SoldProducts)
                .HasForeignKey(e => e.SellerId);
        }
    }
}