using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.Logic.DTOs
{
    public class PlayerDTO : BaseDTO
    {
        public TeamDTO Team { get; set; }
    }
}
