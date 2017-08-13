using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.Logic.DTOs
{
    public class CourseDTO : BaseDTO
    {
        public ICollection<TeamDTO> Teams { get; set; }
    }
}
