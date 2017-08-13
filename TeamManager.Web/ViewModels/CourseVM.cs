using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TeamManager.Web.ViewModels
{
    public class CourseVM
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        //ICollection<int>
        //public IEnumerable<SelectListItem> IdsOfSelectedTeams { get; set; } = new List<SelectListItem>();
        //public MultiSelectList AvailableTeams { get; set; }
        public IEnumerable<SelectableTeamVM> SelectableTeams { get; set; }
    }
}
