namespace P01_BillsPaymentSystem.Application.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.ToTable("BankAccounts");
            
            builder.HasKey(ba => ba.BankAccountId);

            builder.Property(ba => ba.BankName)
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(ba => ba.SWIFTCode)
                .IsUnicode(false)
                .HasMaxLength(20);
        }
    }
}