using TeamManager.Model.Helpers;
using System.Collections.Generic;

namespace TeamManager.Web.ViewModels
{
    public class TeamsPageVM
    {
        public IEnumerable<TeamVM> Teams { get; set; }
        public PageInf PageInf { get; set; }
        //public int CountOfAllTeams { get; set; }
    }
}
