namespace P03_FootballBetting.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;


    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext()
        {
        }


        public FootballBettingContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Color> Colors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.PlayerId);

                entity.Property(e => e.Name)
                    .IsRequired();

                entity.Property(e => e.SquadNumber)
                    .IsRequired();

                entity.HasOne(e => e.Team)
                    .WithMany(p => p.Players)
                    .HasForeignKey(e => e.TeamId);

                entity.HasOne(e => e.Position)
                    .WithMany(p => p.Players)
                    .HasForeignKey(e => e.PositionId);

                entity.Property(e => e.IsInjured)
                    .IsRequired();
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.TeamId);

                entity.Property(e => e.Name)
                    .IsRequired();

                entity.Property(e => e.LogoUrl)
                    .IsRequired();

                entity.Property(e => e.Initials)
                    .IsRequired();

                entity.Property(e => e.Budget)
                    .IsRequired();

                entity.HasOne(e => e.PrimaryKitColor)
                    .WithMany(pkc => pkc.PrimaryKitTeams)
                    .HasForeignKey(e => e.PrimaryKitColorId);

                entity.HasOne(e => e.SecondaryKitColor)
                    .WithMany(pkc => pkc.SecondaryKitTeams)
                    .HasForeignKey(e => e.SecondaryKitColorId);

                entity.HasOne(e => e.Town)
                    .WithMany(t => t.Teams)
                    .HasForeignKey(e => e.TownId);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.PositionId);

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.GameId);

                entity.HasOne(e => e.HomeTeam)
                    .WithMany(h => h.HomeGames)
                    .HasForeignKey(e => e.HomeTeamId);

                entity.HasOne(e => e.AwayTeam)
                    .WithMany(a => a.AwayGames)
                    .HasForeignKey(e => e.AwayTeamId);

                entity.Property(e => e.AwayTeamGoals)
                    .IsRequired();

                entity.Property(e => e.HomeTeamGoals)
                    .IsRequired();

                entity.Property(e => e.DateTime)
                    .IsRequired()
                    .HasColumnType("DATETIME2");

                entity.Property(e => e.HomeTeamBetRate)
                    .IsRequired();

                entity.Property(e => e.AwayTeamBetRate)
                    .IsRequired();

                entity.Property(e => e.DrawBetRate)
                    .IsRequired();

                entity.Property(e => e.Result)
                    .IsRequired();
            });

            modelBuilder.Entity<PlayerStatistic>(entity =>
            {
                entity.HasKey(e => new {e.PlayerId, e.GameId});

                entity.HasOne(e => e.Game)
                    .WithMany(p => p.PlayerStatistics)
                    .HasForeignKey(e => e.GameId);

                entity.HasOne(e => e.Player)
                    .WithMany(p => p.PlayerStatistics)
                    .HasForeignKey(e => e.PlayerId);

                entity.Property(e => e.ScoredGoals)
                    .IsRequired();

                entity.Property(e => e.Assists)
                    .IsRequired();

                entity.Property(e => e.MinutesPlayed)
                    .IsRequired();
            });

            modelBuilder.Entity<Bet>(entity =>
            {
                entity.HasKey(e => e.BetId);

                entity.Property(e => e.Amount)
                    .IsRequired();

                entity.Property(e => e.Prediction)
                    .IsRequired();

                entity.Property(e => e.DateTime)
                    .IsRequired()
                    .HasColumnType("DATETIME2");

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Bets)
                    .HasForeignKey(e => e.UserId);

                entity.HasOne(e => e.Game)
                    .WithMany(g => g.Bets)
                    .HasForeignKey(e => e.GameId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Username).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Balance).IsRequired();
            });

            modelBuilder.Entity<Town>(entity =>
            {
                entity.HasKey(e => e.TownId);

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(e => e.Country)
                    .WithMany(t => t.Towns)
                    .HasForeignKey(e => e.CountryId);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.CountryId);

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(e => e.ColorId);

                entity.Property(e => e.Name).IsRequired();
            });
        }
    }
}