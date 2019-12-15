using DAL;
using Entities;
using BLL.SortEnums;
using BLL.Services.Interfaces;
using System.Collections.Generic;

namespace BLL.Services.Implementations
{
    public class SupplierService : Service<Supplier>, ISupplierService
    {
        public SupplierService() : base(new Repository<Supplier>())
        {
        }

        public SupplierService(IRepository<Supplier> repository) : base(repository)
        {
        }

        public List<Supplier> GetAllOrderBy(SupplierSort sort)
        {
            return base.GetAllBy(sort.ToString());
        }
    }
}
