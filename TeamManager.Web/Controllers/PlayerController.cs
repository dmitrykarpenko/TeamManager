using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TeamManager.Logic;
using TeamManager.Model.Entities;
using TeamManager.Model.Helpers;
using TeamManager.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamManager.Web.Controllers
{
    public class PlayerController : Controller
    {
        private PlayersLogic _playersLogic;
        private TeamsLogic _teamsLogic;
        public PlayerController(PlayersLogic playersLogic, TeamsLogic teamsLogic)
        {
            _playersLogic = playersLogic;
            _teamsLogic = teamsLogic;
        }

        //public static readonly JsonSerializerSettings jsonSerSettings = new JsonSerializerSettings()
        //    {
        //        ContractResolver = new CamelCasePropertyNamesContractResolver()
        //    };

        public ActionResult Index()
        {
            var pageInf = new PageInf() { Page = 1, PageSize = 10 };

            var playersFeed = _playersLogic.GetPlayersFeed(null, pageInf, s => s.Name);
            var availableTeams = _teamsLogic.GetTeams();

            var playerVMs = AutoMapper.Mapper.Map<IEnumerable<PlayerVM>>(playersFeed.Collection);
            var availableTeamVMs = AutoMapper.Mapper.Map<IEnumerable<TeamVM>>(availableTeams);

            var viewModel = new PlayersPageVM()
            {
                Players = playerVMs,
                AvailableTeams = availableTeamVMs,
                PageInf = pageInf,
                CountOfAllPlayers = playersFeed.Count
            };

            return View(viewModel);
        }

        public JsonResult Save(IEnumerable<PlayerVM> playerVMs)
        {
            var players = AutoMapper.Mapper.Map<IEnumerable<Player>>(playerVMs);
            
            var retPlayers = _playersLogic.InsertOrUpdate(players);

            var retPlayerVMs = AutoMapper.Mapper.Map<IEnumerable<PlayerVM>>(retPlayers);

            return Json(new { players = retPlayerVMs });
        }

        public JsonResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                Response.StatusCode = 400;
                return Json(new { error = "Invalid player id!" });
            }
            var retPlayer = _playersLogic.Delete(id);

            return Json(new { });
        }

        public JsonResult GetPage(PageInf pageInf)
        {
            var playersFeed = _playersLogic.GetPlayersFeed(null, pageInf, s => s.Name);
            var players = playersFeed.Collection;
            var availableTeams = _teamsLogic.GetTeams();

            var playerVMs = AutoMapper.Mapper.Map<IEnumerable<PlayerVM>>(players);
            var availableTeamVMs = AutoMapper.Mapper.Map<IEnumerable<TeamVM>>(availableTeams);

            var viewModel = new PlayersPageVM()
            {
                Players = playerVMs,
                AvailableTeams = availableTeamVMs,
                PageInf = pageInf,
                CountOfAllPlayers = playersFeed.Count
            };

            return Json(viewModel);
        }
    }
}