using Moq;
using DAL;
using Xunit;
using Entities;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using BLL.Services.Implementations;

namespace BLL.Test
{
    public class SearchTest
    {
        [Fact]
        public void SearchCategory_KeywordPassed_ProperCollectionReceived()
        {
            // Arrange
            Category category1 = new Category { Id = 1, CategoryName = "Food" };
            Category category2 = new Category { Id = 2, CategoryName = "Drink" };
            Category category3 = new Category { Id = 3, CategoryName = "Food" };

            var categories = new List<Category> { category1, category2, category3 };

            Mock<DbSet<Category>> dbSetMock = CreateDbSetMock<Category>(categories);
            var context = new Mock<DbContext>();
            context.Setup(x => x.Set<Category>()).Returns(dbSetMock.Object);

            // Act
            var repository = new Repository<Category>(context.Object);
            var search = new Search(repository, null, null);
            var actual = search.SearchCategory("oo");

            // Assert
            context.Verify(x => x.Set<Category>());
            Assert.Collection(actual,
                item =>
                {
                    Assert.Equal(1, item.Id);
                    Assert.Equal("Food", item.CategoryName);
                },
                category =>
                {
                    Assert.Equal(3, category.Id);
                    Assert.Equal("Food", category.CategoryName);
                }
                );
        }

        [Fact]
        public void SearchProduct_KeywordPassed_ProperCollectionReceived()
        {
            // Arrange
            Product product1 = new Product { Id = 1, Name = "Name1", Brand = "A" };
            Product product2 = new Product { Id = 2, Name = "Name2", Brand = "B" };
            Product product3 = new Product { Id = 3, Name = "Name3", Brand = "C" };

            var products = new List<Product> { product1, product2, product3 };

            Mock<DbSet<Product>> dbSetMock = CreateDbSetMock<Product>(products);
            var context = new Mock<DbContext>();
            context.Setup(x => x.Set<Product>()).Returns(dbSetMock.Object);

            // Act
            var repository = new Repository<Product>(context.Object);
            var search = new Search(null, repository, null);
            var actual = search.SearchProduct("C");

            // Assert
            context.Verify(x => x.Set<Product>());
            Assert.Collection(actual,
                product =>
                {
                    Assert.Equal(3, product.Id);
                    Assert.Equal("Name3", product.Name);
                }
                );
        }

        [Fact]
        public void SearchSupplier_KeywordPassed_ProperCollectionReceived()
        {
            // Arrange
            Supplier product1 = new Supplier { Id = 1, FirstName = "Name1", LastName = "Cambino" };
            Supplier product2 = new Supplier { Id = 2, FirstName = "Name2", LastName = "Baly" };
            Supplier product3 = new Supplier { Id = 3, FirstName = "Name3", LastName = "Crowly" };

            var products = new List<Supplier> { product1, product2, product3 };

            Mock<DbSet<Supplier>> dbSetMock = CreateDbSetMock<Supplier>(products);
            var context = new Mock<DbContext>();
            context.Setup(x => x.Set<Supplier>()).Returns(dbSetMock.Object);

            // Act
            var repository = new Repository<Supplier>(context.Object);
            var search = new Search(null, null, repository);
            var actual = search.SearchSupplier("wly");

            // Assert
            context.Verify(x => x.Set<Supplier>());
            Assert.Collection(actual,
                supplier =>
                {
                    Assert.Equal(3, supplier.Id);
                    Assert.Equal("Name3", supplier.FirstName);
                    Assert.Equal("Crowly", supplier.LastName);
                }
                );
        }

        private Mock<DbSet<T>> CreateDbSetMock<T>(List<T> list) where T : class
        {
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Provider).Returns(list.AsQueryable().Provider);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(list.AsQueryable().Expression);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(list.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(list.AsQueryable().GetEnumerator());
            return dbSetMock;
        }
    }
}
