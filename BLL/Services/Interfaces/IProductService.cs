using Entities;
using BLL.SortEnums;
using System.Collections.Generic;

namespace BLL.Services.Interfaces
{
    public interface IProductService : IService<Product>
    {
        void AddToCategory(int productId, int categoryId);
        void RemoveFromCategory(int productId);
        List<Product> GetAllOrderBy(ProductSort sort);
    }
}
