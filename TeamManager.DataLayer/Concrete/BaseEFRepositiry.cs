﻿using TeamManager.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using TeamManager.DataLayer.Abstract;
using TeamManager.Model.Helpers;
using TeamManager.DataLayer.Helpers;

namespace TeamManager.DataLayer.Concrete
{
    public class BaseEFRepositiry<T> : IRepository<T> where T : class, IEntity
    {
        internal EFTeamManagerContext _context;
        internal DbSet<T> _dbSet;

        public BaseEFRepositiry(EFTeamManagerContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, PageInf pageInf = null,
                                          Expression<Func<T, object>> include = null,
                                          Expression<Func<T, object>> orderBy = null, bool byDesc = false)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = byDesc ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);

            if (pageInf != null)
                if (pageInf.Page > 0 && pageInf.PageSize > 0)
                {
                    int quanToSkip = (pageInf.Page - 1) * pageInf.PageSize;
                    query = query.Skip(quanToSkip).Take(pageInf.PageSize);
                }
                else
                    throw new ArgumentException("Invalid PageInf", "pageInf");

            if (include != null)
                query = query.Include(include);

            return query.ToList();
        }

        public int Count(Expression<Func<T, bool>> filter = null)
        {
            int count = filter != null ? _dbSet.Count(filter) : _dbSet.Count();
            return count;
        }

        //public virtual void Insert(T entity)
        //{
        //    _dbSet.Add(entity);
        //}

        //public virtual void Delete(object id)
        //{
        //    T entityToDelete = _dbSet.Find(id);
        //    Delete(entityToDelete);
        //}

        public virtual IEnumerable<T> InsertOrUpdate(IEnumerable<T> entities)
        {
            var ret = entities;

            foreach (var entity in entities)
            {
                var entityCollections = ReflectionHelpers.GetCollections<IEntity>(entity);
                //otherwise all items from inner collections will be added and probably duplicated
                foreach (var col in entityCollections)
                    foreach (var el in col)
                        _context.Entry(el).State = el.Id == 0 ? EntityState.Added : EntityState.Unchanged;

                _dbSet.Add(entity);
                _context.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;

                //todo: consider setting all subentities state to added, modified or unchanged by Id
            }

            return ret;
        }

        public virtual IEnumerable<T> Delete(int id)
        {
            return Delete(ent => ent.Id == id);
        }

        public virtual IEnumerable<T> Delete(Expression<Func<T, bool>> filter)
        {
            var entitiesToDelete = _dbSet.Where(filter);
            foreach (var entity in entitiesToDelete)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
            }
            return entitiesToDelete;
        }

        public virtual int Save()
        {
            return _context.SaveChanges();
        }
    }
}
