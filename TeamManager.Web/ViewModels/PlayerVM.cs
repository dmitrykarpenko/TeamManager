using System;
using System.Collections.Generic;

namespace TeamManager.Web.ViewModels
{
    public class PlayerVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TeamVM> Teams { get; set; }
    }
}
