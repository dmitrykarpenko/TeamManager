using TeamManager.DataLayer.Abstract;
using TeamManager.Logic.DTOs;
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
    public class PlayersLogic
    {
        private IUnitOfWork _unitOfWork;

        public PlayersLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Feed<PlayerDTO> GetPlayersFeed(Expression<Func<Player, bool>> filter = null, PageInf pageInf = null,
                                                Expression<Func<Player, object>> orderBy = null, bool byDesc = false)
        {
            var players = GetPlayers(filter, pageInf, orderBy, byDesc).ToList();
            var playerDTOs = AutoMapper.Mapper.Map<IEnumerable<PlayerDTO>>(players); ;

            var feed = new Feed<PlayerDTO>() { Collection = playerDTOs };

            if (pageInf != null && pageInf.IsValid())
            {
                feed.Skipped = (pageInf.Page - 1) * pageInf.PageSize;

                int playersCount = playerDTOs.Count();
                if (playersCount < pageInf.Page)
                    feed.Count = playersCount;
            }
            else
            {
                feed.Skipped = 0;
            }

            if (feed.Count == 0)
            {
                var playersRepo = _unitOfWork.GetRepositiry<Player>();
                feed.Count = playersRepo.Count(filter);
            }

            return feed;
        }

        public IEnumerable<Player> GetPlayers(Expression<Func<Player, bool>> filter = null, PageInf pageInf = null,
                                                Expression<Func<Player, object>> orderBy = null, bool byDesc = false)
        {
            var playersRepo = _unitOfWork.GetRepositiry<Player>();

            var players = playersRepo.Get(filter, pageInf, s => s.Teams, orderBy, byDesc);

            return players;
        }

        public virtual IEnumerable<Player> InsertOrUpdate(IEnumerable<Player> players)
        {
            //preserve teams
            var teamsArr = players.Select(s => s.Teams).ToArray();
            //set teams to null to update only players data
            foreach (var player in players)
                player.Teams = null;

            var playersRepo = _unitOfWork.GetRepositiry<Player>();
            playersRepo.InsertOrUpdate(players);
            _unitOfWork.Save();

            var insOrUpdPlayersArr = players.ToArray();

            //set teams for updated players
            for (int i = 0; i < Math.Max(teamsArr.Length, insOrUpdPlayersArr.Length); ++i)
                insOrUpdPlayersArr[i].Teams = teamsArr[i];

            return players;
        }

        public virtual IEnumerable<Player> Delete(Guid playerId)
        {
            var playersRepo = _unitOfWork.GetRepositiry<Player>();
            var retPlayers = playersRepo.Delete(playerId);
            _unitOfWork.Save();

            return retPlayers;
        }
    }
}
