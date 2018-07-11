namespace P01_StudentSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {
        }


        public StudentSystemContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Homework> HomeworkSubmissions { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(100);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired(false)
                    .IsUnicode(false)
                    .HasColumnType("CHAR(10)");


                entity.Property(e => e.RegisteredOn)
                    .IsRequired();

                entity.Property(e => e.Birthday)
                    .IsRequired(false)
                    .HasColumnType("DATETIME2");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(80);

                entity.Property(e => e.Description)
                    .IsRequired(false)
                    .IsUnicode();

                entity.Property(e => e.StartDate)
                    .IsRequired()
                    .HasColumnType("DATETIME2");

                entity.Property(e => e.EndDate)
                    .IsRequired()
                    .HasColumnType("DATETIME2");

                entity.Property(e => e.Price)
                    .IsRequired();
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasKey(e => e.ResourceId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(50);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.ResourceType)
                    .IsRequired();

                entity.HasOne(e => e.Course)
                    .WithMany(e => e.Resources)
                    .HasForeignKey(e => e.CourseId);
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                entity.HasKey(e => e.HomeworkId);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.ContentType)
                    .IsRequired();

                entity.Property(e => e.SubmissionTime)
                    .IsRequired();

                entity.HasOne(e => e.Student)
                    .WithMany(s => s.HomeworkSubmissions)
                    .HasForeignKey(e => e.StudentId);

                entity.HasOne(e => e.Course)
                    .WithMany(c => c.HomeworkSubmissions)
                    .HasForeignKey(e => e.CourseId);
            });


            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(e => new {e.CourseId, e.StudentId});

                entity.HasOne(e => e.Student)
                    .WithMany(s => s.CourseEnrollments)
                    .HasForeignKey(e => e.StudentId);

                entity.HasOne(e => e.Course)
                    .WithMany(c => c.StudentsEnrolled)
                    .HasForeignKey(e => e.CourseId);
            });
        }
    }
}