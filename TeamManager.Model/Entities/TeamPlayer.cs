using System;

namespace TeamManager.Model.Entities
{
    public class TeamPlayer
    {
        public Guid PlayerId { get; set; }
        public Guid CourseId { get; set; }
        public virtual Player Player { get; set; }
        public virtual Course Course { get; set; }
    }
}
