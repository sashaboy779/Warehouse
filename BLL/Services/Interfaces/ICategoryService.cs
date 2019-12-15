using Entities;
using System.Collections.Generic;

namespace BLL.Services.Interfaces
{
    public interface ICategoryService : IService<Category>
    {
        List<Category> GetAll();
    }
}
