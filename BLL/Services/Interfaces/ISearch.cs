using Entities;
using System.Collections.Generic;

namespace BLL.Services.Interfaces
{
    public interface ISearch
    {
        List<Category> SearchCategory(string keyWord);
        List<Product> SearchProduct(string keyWord);
        List<Supplier> SearchSupplier(string keyWord);
    }
}