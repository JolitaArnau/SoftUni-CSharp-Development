namespace P01_BillsPaymentSystem.Application.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class UserConfiguration : IEntityTypeConfiguration<User>

    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.UserId);

            builder.Property(u => u.FirstName)
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsUnicode(false)
                .HasMaxLength(80);

            builder.Property(u => u.Password)
                .IsUnicode(false)
                .HasMaxLength(25);
        }
    }
}