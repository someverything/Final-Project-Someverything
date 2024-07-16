﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    public class EFRepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using var context = new TContext();
            var AddEntity = context.Entry(entity);
            AddEntity.State = EntityState.Added;
            context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            using var context = new TContext();
            var DeleteEntity = context.Entry(entity);
            DeleteEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            using var context = new TContext();
            return context.Set<TEntity>().FirstOrDefault(expression);
        }

        public void Update(TEntity entity)
        {
            using var context = new TContext();
            var UpdateEntity = context.Entry(entity);
            UpdateEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
