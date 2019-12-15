using DAL;
using Entities;
using System.Linq;
using BLL.Services.Interfaces;
using System.Collections.Generic;

namespace BLL.Services.Implementations
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        public CategoryService() : base(new Repository<Category>())
        {
        }

        public CategoryService(IRepository<Category> repository) : base(repository)
        {
        }

        public List<Category> GetAll()
        {
            return Repository.GetAll().ToList();
        }
    }
}
