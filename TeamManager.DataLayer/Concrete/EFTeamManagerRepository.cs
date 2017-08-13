using TeamManager.DataLayer.Abstract;
using TeamManager.Model.Entities;
using TeamManager.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.DataLayer.Concrete
{
    public class EFTeamManagerRepository : ITeamManagerRepository
    {
        EFTeamManagerContext _context;
        public EFTeamManagerRepository()
        {
            _context = new EFTeamManagerContext();
        }

        public IEnumerable<Player> GetPlayers(Expression<Func<Player, bool>> condition, Expression<Func<Player, object>> orderBy, PageInf pageInf)
        {
            int quanToSkip = pageInf != null && pageInf.Page > 1 && pageInf.PageSize > 0 ? (pageInf.Page - 1) * pageInf.Page : 0;

            var players = _context.Players.Where(condition).OrderBy(orderBy)
                                   .Skip(quanToSkip).Take(pageInf.PageSize).Include(s => s.Group).ToList();
            return players;
        }
    }
}
