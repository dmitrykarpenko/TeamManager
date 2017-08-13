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
    public class CourseController : Controller
    {
        private CoursesLogic _coursesLogic;
        private TeamsLogic _teamsLogic;
        public CourseController(CoursesLogic coursesLogic, TeamsLogic teamsLogic)
        {
            _coursesLogic = coursesLogic;
            _teamsLogic = teamsLogic;
        }

        public ActionResult ShowAll()
        {
            var pageInf = new PageInf() { Page = 1, PageSize = 10 };

            var courses = _coursesLogic.GetCourses(null, pageInf, s => s.Name);

            var courseVMs = AutoMapper.Mapper.Map<IEnumerable<CourseVM>>(courses);

            var viewModel = new CoursesPageVM()
            {
                Courses = courseVMs,
                PageInf = pageInf
            };

            return View(viewModel);
        }

        public ActionResult AddNewOrEdit(Guid? id)
        {
            CourseVM vm = null;
            IEnumerable<Team> notSelectedTeams = null;

            if (id != null)
            {
                Guid idVal = id.GetValueOrDefault();
                var course = _coursesLogic.GetCourses(c => c.Id == idVal).FirstOrDefault();
                if (course != null)
                {
                    vm = AutoMapper.Mapper.Map<CourseVM>(course);
                    if (vm.SelectableTeams != null && vm.SelectableTeams.Any())
                    {
                        foreach (var sg in vm.SelectableTeams)
                            sg.Selected = true;

                        var selectedTeamIds = vm.SelectableTeams.Select(sg => sg.Id).ToList();
                        notSelectedTeams = _teamsLogic.GetTeams(g => !selectedTeamIds.Contains(g.Id));
                    }
                }
            }

            if (vm == null)
                vm = new CourseVM() { SelectableTeams = new List<SelectableTeamVM>() };

            if (notSelectedTeams == null)
                notSelectedTeams = _teamsLogic.GetTeams();

            ((List<SelectableTeamVM>)vm.SelectableTeams)
                .AddRange(AutoMapper.Mapper.Map<IEnumerable<SelectableTeamVM>>(notSelectedTeams));

            return View(vm);
        }

        [HttpPost]
        public ActionResult AddNewOrEdit(CourseVM courseVM)
        {
            var course = AutoMapper.Mapper.Map<Course>(courseVM);

            _coursesLogic.InsertOrUpdate(new List<Course>() { course });

            return RedirectToAction("ShowAll");
        }

        public JsonResult Save(IEnumerable<CourseVM> courseVMs)
        {
            var courses = AutoMapper.Mapper.Map<IEnumerable<Course>>(courseVMs);
            
            _coursesLogic.InsertOrUpdate(courses);

            courseVMs = AutoMapper.Mapper.Map<IEnumerable<CourseVM>>(courses);

            return Json(new { courses = courseVMs });
        }

        public JsonResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                Response.StatusCode = 400;
                return Json(new { error = "Invalid course id!" });
            }
            var retPlayer = _coursesLogic.Delete(id);

            return Json(new { });
        }

        public JsonResult GetPage(PageInf pageInf)
        {
            var courses = _coursesLogic.GetCourses(null, pageInf, s => s.Name);

            var courseVMs = AutoMapper.Mapper.Map<IEnumerable<CourseVM>>(courses);

            var viewModel = new CoursesPageVM()
            {
                Courses = courseVMs,
                PageInf = pageInf
            };

            return Json(viewModel);
        }
    }
}