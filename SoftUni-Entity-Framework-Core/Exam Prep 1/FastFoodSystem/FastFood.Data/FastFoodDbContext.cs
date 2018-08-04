using FastFood.Models;
using FastFood.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Data
{
    public class FastFoodDbContext : DbContext
    {
        public FastFoodDbContext()
        {
        }

        public FastFoodDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Age)
                    .IsRequired();

                entity.HasMany(e => e.Orders)
                    .WithOne(e => e.Employee)
                    .HasForeignKey(e => e.EmployeeId);
            });

            builder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired();

                entity.HasMany(e => e.Employees)
                    .WithOne(e => e.Position)
                    .HasForeignKey(e => e.PositionId);
            });

            builder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasMany(e => e.Items)
                    .WithOne(e => e.Category)
                    .HasForeignKey(e => e.CategoryId);
            });

            builder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Price)
                    .IsRequired();
            });

            builder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Customer)
                    .IsRequired();

                entity.Property(e => e.DateTime)
                    .IsRequired()
                    .HasColumnType("DATETIME2");

                entity.Property(e => e.Type)
                    .HasDefaultValue(Type.ForHere)
                    .IsRequired();

                entity.Ignore(e => e.TotalPrice);
            });

            builder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => new {e.ItemId, e.OrderId});
                
                entity.HasOne(e => e.Order)
                    .WithMany(e => e.OrderItems)
                    .HasForeignKey(e => e.OrderId);

                entity.HasOne(e => e.Item)
                    .WithMany(e => e.OrderItems)
                    .HasForeignKey(e => e.ItemId);

                entity.Property(e => e.Quantity)
                    .IsRequired();
            });
        }
    }
}