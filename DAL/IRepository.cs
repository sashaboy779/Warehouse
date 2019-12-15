using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace DAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Remove(int id);
        TEntity Get(int id);
        void Update(TEntity updatedEntity, int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}
