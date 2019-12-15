using Entities;
using BLL.SortEnums;
using System.Collections.Generic;

namespace BLL.Services.Interfaces
{
    public interface ISupplierService : IService<Supplier>
    {
        List<Supplier> GetAllOrderBy(SupplierSort sort);
    }
}
