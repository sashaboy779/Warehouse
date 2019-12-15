using BLL.Services.Interfaces;

namespace PL.Menu.Interfaces
{
    public interface ISubmenu<T> where T : class
    {
        void Add(IService<T> service, T entity, string successMessage);
        void Remove(IService<T> service, int id, string successMessage);
        void Update(IService<T> service, T updatedEntity, int id, string successMessage);
        void Show(IService<T> service, int id, string failMessage);
    }
}
