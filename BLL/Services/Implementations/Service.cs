using DAL;
using System;
using System.Linq;
using BLL.Resources;
using System.Reflection;
using BLL.Services.Interfaces;
using System.Collections.Generic;

namespace BLL.Services.Implementations
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        protected IRepository<TEntity> Repository;

        public Service(IRepository<TEntity> repository)
        {
            Repository = repository;
        }
        public void Add(TEntity entity)
        {
            try
            {
                Repository.Add(entity);
            }
            catch (Exception)
            {
                throw new Exception(ExceptionMessages.UnableToAdd);
            }
        }

        public TEntity Get(int id)
        {
            return Repository.Get(id);
        }

        public void Remove(int id)
        {
            try
            {
                Repository.Remove(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(TEntity entity, int id)
        {
            try
            {
                Repository.Update(entity, id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TEntity> GetAllBy(string orderOption)
        {
            PropertyInfo pi = typeof(TEntity).GetProperty(orderOption);
            return Repository.GetAll().OrderBy(x => pi.GetValue(x, null)).ToList();
        }
    }
}
