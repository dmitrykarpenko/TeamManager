using TeamManager.DataLayer.Configurations;
using TeamManager.Model.Entities;
using System.Data.Entity;
using MC = System.Data.Entity.ModelConfiguration;

namespace TeamManager.DataLayer.Concrete
{
    public class EFTeamManagerContext : DbContext
    {
        public EFTeamManagerContext() : base("DefaultConnection")
        {
            //Database.SetInitializer<EFTeamManagerContext>(null);
        }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EFTeamManagerContext>(null);

            base.OnModelCreating(modelBuilder);

            // Players

            modelBuilder.Configurations.Add(new PlayersConfiguration());

            // Teams

            modelBuilder.Configurations.Add(new TeamsConfiguration());

            modelBuilder.Entity<Team>()
                .HasMany(g => g.Players).WithMany(c => c.Teams)
                .Map(x =>
                {
                    x.MapLeftKey("TeamId");
                    x.MapRightKey("PlayerId");
                    x.ToTable("TeamPlayers");
                });
        }
    }
}
