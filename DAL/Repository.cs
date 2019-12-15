using System;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace DAL
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> Entities;

        public Repository()
        {
            Context = new WarehouseContext();
            Entities = Context.Set<TEntity>();
        }

        public Repository(DbContext context)
        {
            Context = context;
            Entities = Context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            Entities.Add(entity);
            Context.SaveChanges();
        }

        public void Remove(int id)
        {
            var entityToRemove = Entities.Find(id);
            if (entityToRemove == null)
            {
                throw new Exception($"Unable to remove: There is no object with id: {id}");
            }
            Entities.Remove(entityToRemove);
            Context.SaveChanges();
        }

        public void Update(TEntity updatedEntity, int entityId)
        {
            var oldEntity = Entities.Find(entityId);

            if (oldEntity == null)
            {
                throw new Exception($"Updating error: There is no object with id: {entityId}");
            }
            Context.Entry(oldEntity).CurrentValues.SetValues(updatedEntity);
            Context.SaveChanges();
        }

        public TEntity Get(int id)
        {
            return Entities.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Entities.ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate);
        }
    }
}
