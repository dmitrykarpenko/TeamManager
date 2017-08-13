using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.Model.Entities
{
    public class Player : BaseEntity
    {
        public virtual ICollection<Team> Teams { get; set; }
    }
}
