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
        
        //public DbSet<Course> Courses { get; set; }
        // public DbSet<TeamCourse> TeamCourses { get; set; } // many-to-many relationship table, not yet created

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EFTeamManagerContext>(null);

            base.OnModelCreating(modelBuilder);

            // Players

            modelBuilder.Configurations.Add(new PlayersConfiguration());

            // Teams

            modelBuilder.Configurations.Add(new TeamsConfiguration());

            modelBuilder.Entity<Team>()
                .HasMany(g => g.Courses).WithMany(c => c.Teams)
                .Map(x =>
                {
                    x.MapLeftKey("TeamId");
                    x.MapRightKey("CourseId");
                    x.ToTable("TeamCourses");
                });

            modelBuilder.Entity<Team>()
                .HasMany(g => g.Players).WithMany(c => c.Teams)
                .Map(x =>
                {
                    x.MapLeftKey("TeamId");
                    x.MapRightKey("PlayerId");
                    x.ToTable("TeamPlayers");
                });

            // Courses

            modelBuilder.Configurations.Add(new CoursesConfiguration());

            //// TeamCourses
            ////EntityTypeConfiguration<Team>

            //var teamCoursesConfig = new MC.EntityTypeConfiguration<TeamCourse>()
            //    .HasRequired(gc => gc.Team).WithMany(g => g.Courses).HasForeignKey(g => g.CourseId);

            //modelBuilder.Configurations.Add();
        }
    }
}
