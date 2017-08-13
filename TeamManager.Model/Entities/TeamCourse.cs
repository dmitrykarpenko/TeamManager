using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.Model.Entities
{
    public class TeamCourse
    {
        public int TeamId { get; set; }
        public int CourseId { get; set; }
        public virtual Team Team { get; set; }
        public virtual Course Course { get; set; }
    }
}
