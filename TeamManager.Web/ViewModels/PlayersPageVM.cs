using TeamManager.Model.Helpers;
using System.Collections.Generic;

namespace TeamManager.Web.ViewModels
{
    public class PlayersPageVM
    {
        public IEnumerable<PlayerVM> Players { get; set; }
        public IEnumerable<TeamVM> AvailableTeams { get; set; }
        public PageInf PageInf { get; set; }
        public int CountOfAllPlayers { get; set; }

        public Dictionary<string, int> PlayerPositionEnum { get; set; }
    }
}
