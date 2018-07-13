namespace P01_HospitalDatabase.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using P01_HospitalDatabase.Data.Models;

    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {
        }

        public HospitalContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<PatientMedicament> Prescriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            //base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.PatientId);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode();

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.HasInsurance)
                    .IsRequired()
                    .HasDefaultValue(true);
            });

            modelBuilder.Entity<Visitation>(entity =>
            {
                entity.HasKey(e => e.VisitationId);

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasColumnType("DATETIME2")
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.Comments)
                    .IsRequired(false)
                    .IsUnicode()
                    .HasMaxLength(250);

                entity.HasOne(e => e.Patient)
                    .WithMany(e => e.Visitations)
                    .HasForeignKey(e => e.PatientId)
                    .HasConstraintName("FK_Visitaion_Patient");

                entity.HasOne(e => e.Doctor)
                    .WithMany(e => e.Visitations)
                    .HasForeignKey(e => e.DoctorId)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Diagnose>(entity =>
            {
                entity.HasKey(e => e.DiagnoseId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(50);

                entity.Property(e => e.Comments)
                    .IsRequired(false)
                    .IsUnicode()
                    .HasMaxLength(250);

                entity.HasOne(e => e.Patient)
                    .WithMany(e => e.Diagnoses)
                    .HasForeignKey(e => e.PatientId)
                    .HasConstraintName("FK_Diagnose_Patient");
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.MedicamentId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PatientMedicament>(entity =>
            {
                entity.HasKey(e => new {e.PatientId, e.MedicamentId});

                entity.HasOne(e => e.Medicament)
                    .WithMany(e => e.Prescriptions)
                    .HasForeignKey(e => e.MedicamentId)
                    .HasConstraintName("FK_PatientMedicament_Patients");

                entity.HasOne(e => e.Patient)
                    .WithMany(e => e.Prescriptions)
                    .HasForeignKey(e => e.PatientId)
                    .HasConstraintName("FK_PatientMedicament_Medicaments");
                ;
            });

            modelBuilder.Entity<Doctor>(entity =>
            {                
                entity.HasKey(e => e.DoctorId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(100);
                
                entity.Property(e => e.Specialty)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(100);
            });
        }
    }
}