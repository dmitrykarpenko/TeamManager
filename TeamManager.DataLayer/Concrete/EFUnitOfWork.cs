using TeamManager.DataLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Model.Entities;
using System.Collections.Concurrent;

namespace TeamManager.DataLayer.Concrete
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private EFTeamManagerContext _context = new EFTeamManagerContext();
        private ConcurrentDictionary<Type, object> _repos = new ConcurrentDictionary<Type, object>();

        //all entities which could have a repository
        private static readonly List<Type> _entitiesWithRepos = new List<Type>() { typeof(Player), typeof(Team), typeof(Course) };

        public IRepository<T> GetRepositiry<T>() where T : class, IEntity
        {
            Type repoType = typeof(T);
            if (!_entitiesWithRepos.Contains(repoType))
                throw new ArgumentException("Invalid type: " + repoType.Name);

            object repo = _repos.GetOrAdd(repoType, new BaseEFRepositiry<T>(_context));

            return (IRepository<T>)repo;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        #region dispose
        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                _context.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
