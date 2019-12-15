using DAL;
using Entities;
using System.Linq;
using BLL.Services.Interfaces;
using System.Collections.Generic;

namespace BLL.Services.Implementations
{
    public class Search : ISearch
    {
        private readonly IRepository<Category> category;
        private readonly IRepository<Product> product;
        private readonly IRepository<Supplier> supplier;

        public Search()
        {
            category = new Repository<Category>();
            product = new Repository<Product>();
            supplier = new Repository<Supplier>();
        }

        public Search(IRepository<Category> category, IRepository<Product> product, IRepository<Supplier> supplier)
        {
            this.category = category;
            this.product = product;
            this.supplier = supplier;
        }

        public List<Category> SearchCategory(string keyWord)
        {
            return category.Find(c => c.CategoryName.Contains(keyWord)).ToList();
        }

        public List<Product> SearchProduct(string keyWord)
        {
            return product.Find(p => p.Name.Contains(keyWord) || p.Brand.Contains(keyWord)).ToList();
        }

        public List<Supplier> SearchSupplier(string keyWord)
        {
            return supplier.Find(s => s.FirstName.Contains(keyWord) || s.LastName.Contains(keyWord)).ToList();
        }
    }
}
