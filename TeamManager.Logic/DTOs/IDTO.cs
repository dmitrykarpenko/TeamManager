using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.Logic.DTOs
{
    public interface IDTO
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}
