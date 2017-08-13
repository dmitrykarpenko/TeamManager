using TeamManager.DataLayer.Abstract;
using TeamManager.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.DataLayer.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepositiry<T>() where T : class, IEntity;
        void Save();
    }
}
