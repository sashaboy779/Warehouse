using DAL;
using System;
using Entities;
using BLL.SortEnums;
using BLL.Resources;
using BLL.Services.Interfaces;
using System.Collections.Generic;

namespace BLL.Services.Implementations
{
    public class ProductService : Service<Product>, IProductService
    {
        public ProductService() : base(new Repository<Product>())
        {
        }

        public ProductService(IRepository<Product> repository) : base(repository)
        {
        }

        public new void Add(Product product)
        {
            try
            {
                var categoryService = new CategoryService();
                var supplierService = new SupplierService();

                if (categoryService.Get((int)product.CategoryId) == null)
                {
                    throw new Exception(ExceptionMessages.NoCategoryId);
                }

                if (supplierService.Get((int)product.SupplierId) == null)
                {
                    throw new Exception(ExceptionMessages.NoSupplierId);
                }
                Repository.Add(product);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddToCategory(int productId, int categoryId)
        {
            try
            {
                var product = Repository.Get(productId);

                if (product.CategoryId != null)
                {
                    throw new Exception(ExceptionMessages.HasCategory);
                }

                product.CategoryId = categoryId;
                Repository.Update(product, product.Id);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<Product> GetAllOrderBy(ProductSort sort)
        {
            return base.GetAllBy(sort.ToString());
        }

        public void RemoveFromCategory(int productId)
        {
            try
            {
                var product = Repository.Get(productId);

                product.Category = null;
                product.CategoryId = null;
                Repository.Update(product, product.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
