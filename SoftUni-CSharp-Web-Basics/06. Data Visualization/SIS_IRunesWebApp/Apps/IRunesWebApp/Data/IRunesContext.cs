namespace IRunesWebApp.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    
    public class IRunesContext : DbContext
    {
        private static string ConnectionString => "Server=127.0.0.1,1433;Database=IRunesWebApp;Integrated Security=true";
        
        public DbSet<User> Users { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Track> Tracks { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(ConnectionString)
                .UseLazyLoadingProxies();

            base.OnConfiguring(optionsBuilder);
        }
    }
}