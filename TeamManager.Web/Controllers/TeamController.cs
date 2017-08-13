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
    public class TeamController : Controller
    {
        private TeamsLogic _teamsLogic;
        public TeamController(TeamsLogic teamsLogic)
        {
            _teamsLogic = teamsLogic;
        }

        public ActionResult ShowAll()
        {
            var pageInf = new PageInf() { Page = 1, PageSize = 10 };

            var teams = _teamsLogic.GetTeams(null, pageInf, s => s.Name);

            var teamVMs = AutoMapper.Mapper.Map<IEnumerable<TeamVM>>(teams);

            var viewModel = new TeamsPageVM()
            {
                Teams = teamVMs,
                PageInf = pageInf
            };

            return View(viewModel);
        }

        public JsonResult Save(IEnumerable<TeamVM> teamVMs)
        {
            var teams = AutoMapper.Mapper.Map<IEnumerable<Team>>(teamVMs);
            
            _teamsLogic.InsertOrUpdate(teams);

            teamVMs = AutoMapper.Mapper.Map<IEnumerable<TeamVM>>(teams);

            return Json(new { teams = teamVMs });
        }

        public JsonResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                Response.StatusCode = 400;
                return Json(new { error = "Invalid team id!" });
            }
            var retPlayer = _teamsLogic.Delete(id);

            return Json(new { });
        }

        public JsonResult GetPage(PageInf pageInf)
        {
            var teams = _teamsLogic.GetTeams(null, pageInf, s => s.Name);

            var teamVMs = AutoMapper.Mapper.Map<IEnumerable<TeamVM>>(teams);

            var viewModel = new TeamsPageVM()
            {
                Teams = teamVMs,
                PageInf = pageInf
            };

            return Json(viewModel);
        }
    }
}