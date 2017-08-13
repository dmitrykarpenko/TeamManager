﻿using TeamManager.Model.Entities;
using TeamManager.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.DataLayer.Abstract
{
    public interface IRepository<T> where T : class, IEntity
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, PageInf pageInf = null,
                           Expression<Func<T, object>> include = null,
                           Expression<Func<T, object>> orderBy = null, bool byDesc = false);
        int Count(Expression<Func<T, bool>> filter = null);
        IEnumerable<T> InsertOrUpdate(IEnumerable<T> entities);
        IEnumerable<T> Delete(int id);
        IEnumerable<T> Delete(Expression<Func<T, bool>> filter);
        int Save();
    }
}
