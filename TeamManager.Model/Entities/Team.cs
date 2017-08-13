using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.Model.Entities
{
    public class Team : BaseEntity
    {
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
