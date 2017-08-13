﻿using System;

namespace TeamManager.Model.Entities
{
    public class TeamCourse
    {
        public Guid TeamId { get; set; }
        public Guid CourseId { get; set; }
        public virtual Team Team { get; set; }
        public virtual Course Course { get; set; }
    }
}
