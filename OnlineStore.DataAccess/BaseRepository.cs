﻿using OnlineStore.Common;
using OnlineStore.DataAccess.EntityFramework.Context;

namespace OnlineStore.DataAccess 
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly StoreDataBaseContext Context;

        public BaseRepository(StoreDataBaseContext context)
        {
            this.Context = context;
        }

        public IQueryable<TEntity> Get()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public TEntity Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return entity;
        }

        public TEntity Update(TEntity entitty)
        {
            Context.Set<TEntity>().Update(entitty);

            return entitty;
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }
    }
}
