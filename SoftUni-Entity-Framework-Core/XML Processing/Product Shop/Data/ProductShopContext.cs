namespace Product_Shop.Data
{
    using Microsoft.EntityFrameworkCore;
    
    using Configurations;
    using Models;
    
    public class ProductShopContext : DbContext
    {
        public ProductShopContext()
        {
        }


        public ProductShopContext(DbContextOptions options)
            : base(options)
        {
        }


        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CategoryProduct> CatrgoriesProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            modelBuilder.ApplyConfiguration(new CategoryProductConfiguration());
        }
    }
}