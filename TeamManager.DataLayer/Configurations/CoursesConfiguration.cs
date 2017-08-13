using TeamManager.Model.Entities;
using System.Data.Entity.ModelConfiguration;

namespace TeamManager.DataLayer.Configurations
{
    public class CoursesConfiguration : EntityTypeConfiguration<Course>
    {
        public CoursesConfiguration()
        {
            Property(p => p.Name).HasMaxLength(100).IsRequired();
        }
    }
}
