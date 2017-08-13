using TeamManager.Model.Entities;
using System.Data.Entity.ModelConfiguration;

namespace TeamManager.DataLayer.Configurations
{
    public class TeamsConfiguration : EntityTypeConfiguration<Team>
    {
        public TeamsConfiguration()
        {
            Property(p => p.Name).HasMaxLength(100).IsRequired();
        }
    }
}
