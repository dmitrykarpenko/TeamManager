using System;

namespace TeamManager.Model.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}
