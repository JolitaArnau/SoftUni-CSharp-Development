namespace AutoMappingObjects.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class EmployeesDbContext : DbContext
    {
       
        public EmployeesDbContext(DbContextOptions options)
            :base(options) { }

        public DbSet<Employee> Employees { get; set; }

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

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(50);

                entity.Property(e => e.Salary)
                    .IsRequired();

                entity.Property(e => e.Birthday)
                    .HasColumnType("DATETIME2");

                entity.Property(e => e.Address)
                    .IsRequired(false)
                    .HasMaxLength(200);

                entity.HasOne(e => e.Manager)
                    .WithMany(e => e.ManagedEmployees)
                    .HasForeignKey(e => e.ManagerId);
            });   
        }
    }
}