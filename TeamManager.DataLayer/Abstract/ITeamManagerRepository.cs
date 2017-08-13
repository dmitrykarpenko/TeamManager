using TeamManager.Model.Entities;
using TeamManager.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.DataLayer.Abstract
{
    public interface ITeamManagerRepository
    {
        IEnumerable<Player> GetPlayers(Expression<Func<Player, bool>> wherePredicate,
                                         Expression<Func<Player, object>> orderByPredicate, PageInf pageInf);
        IEnumerable<Player> GetPlayers(Expression<Func<Player, bool>> wherePredicate,
                                         Expression<Func<Player, object>> orderByPredicate, bool orderByDesc, PageInf pageInf);
    }
}
