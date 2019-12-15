using System.Collections.Generic;

namespace BLL.Services.Interfaces
{
    public interface IService<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Remove(int id);
        void Update(TEntity entity, int id);
        TEntity Get(int id);
        List<TEntity> GetAllBy(string orderOption);
    }
}
