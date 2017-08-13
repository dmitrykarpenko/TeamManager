using System;

namespace TeamManager.Web.ViewModels
{
    public class PlayerVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TeamVM Team { get; set; }
    }
}
