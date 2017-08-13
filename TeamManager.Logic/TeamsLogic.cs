using TeamManager.DataLayer.Abstract;
using TeamManager.Model.Entities;
using TeamManager.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.Logic
{
    public class TeamsLogic
    {
        private IUnitOfWork _unitOfWork;

        public TeamsLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Team> GetTeams(Expression<Func<Team, bool>> filter = null, PageInf pageInf = null,
                                            Expression<Func<Team, object>> orderBy = null, bool byDesc = false)
        {
            var teamsRepo = _unitOfWork.GetRepositiry<Team>();

            var teams = teamsRepo.Get(filter, pageInf, null, orderBy, byDesc);

            return teams;
        }

        public virtual IEnumerable<Team> InsertOrUpdate(IEnumerable<Team> teams)
        {
            var teamsRepo = _unitOfWork.GetRepositiry<Team>();
            var insOrUpdTeams = teamsRepo.InsertOrUpdate(teams);
            _unitOfWork.Save();

            return insOrUpdTeams;
        }

        public virtual IEnumerable<Team> Delete(Guid teamId)
        {
            var teamsRepo = _unitOfWork.GetRepositiry<Team>();
            var retTeams = teamsRepo.Delete(teamId);
            _unitOfWork.Save();

            return retTeams;
        }
    }
}
