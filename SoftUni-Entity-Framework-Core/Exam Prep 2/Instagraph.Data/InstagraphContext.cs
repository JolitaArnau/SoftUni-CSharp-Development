using Instagraph.Models;
using Microsoft.EntityFrameworkCore;

namespace Instagraph.Data
{
    public class InstagraphContext : DbContext
    {
        public InstagraphContext()
        {
        }

        public InstagraphContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserFollower> UsersFollowers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Picture>(entity =>
            {
                entity.HasMany(e => e.Posts)
                    .WithOne(u => u.Picture)
                    .HasForeignKey(e => e.PictureId)
                    .OnDelete(DeleteBehavior.Restrict);
                ;
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasAlternateKey(e => e.Username);

                entity.HasOne(e => e.ProfilePicture)
                    .WithMany(u => u.Users)
                    .HasForeignKey(e => e.ProfilePictureId)
                    .OnDelete(DeleteBehavior.Restrict);
                

                entity.HasMany(e => e.Followers)
                    .WithOne(u => u.User)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.UsersFollowing)
                    .WithOne(u => u.Follower)
                    .HasForeignKey(u => u.FollowerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Posts)
                    .WithOne(u => u.User)
                    .HasForeignKey(u => u.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Comments)
                    .WithOne(u => u.User)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasMany(e => e.Comments)
                    .WithOne(u => u.Post)
                    .HasForeignKey(e => e.PostId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UserFollower>(entity => { entity.HasKey(e => new {e.UserId, e.FollowerId}); });
        }
    }
}