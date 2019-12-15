using Moq;
using DAL;
using Xunit;
using Entities;
using System.Collections.Generic;
using BLL.Services.Implementations;

namespace BLL.Test
{
    public class ProductServiceTest
    {
        [Fact]
        public void AddToCategory_ProductPassed_ProperMethodCalled()
        {
            // Arrange
            Product product = new Product { Id = 1, Name = "Product", Cost  = 0};

            var mockRepository = new Mock<IRepository<Product>>();
            mockRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(product);
            mockRepository.Setup(y => y.Update(It.IsAny<Product>(), It.IsAny<int>()));

            // Act
            var service = new ProductService(mockRepository.Object);
            service.AddToCategory(product.Id, 5);

            // Assert
            mockRepository.Verify(x => x.Get(It.Is<int>(y => y == 1)));
            mockRepository.Verify(x => x.Update(It.Is<Product>(y => y.CategoryId == 5),
                It.Is<int>(z => z == product.Id)));
        }

        [Fact]
        public void RemoveFromCategory_ProductPassed_ProperMethodCalled()
        {
            // Arrange
            Product product = new Product { Id = 1, Name = "Product", Category = new Category(), CategoryId = 10 };

            var mockRepository = new Mock<IRepository<Product>>();
            mockRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(product);
            mockRepository.Setup(y => y.Update(It.IsAny<Product>(), It.IsAny<int>()));

            // Act
            var service = new ProductService(mockRepository.Object);
            service.RemoveFromCategory(product.Id);

            // Assert
            mockRepository.Verify(x => x.Get(It.Is<int>(y => y == 1)));
            mockRepository.Verify(x => x.Update(It.Is<Product>(y => y == product && product.CategoryId == null && product.Category == null), 
                It.Is<int>(z => z == product.Id)));
        }

        [Fact]
        public void GetAll_TestObjectPassed_ProperMethodCalled()
        {
            // Arrange
            List<Category> testObjects = new List<Category>
            {
                new Category{ CategoryName = "Name1"},
                new Category{ CategoryName = "Name2"},
                new Category{ CategoryName = "Name3"}
            };

            var mockRepository = new Mock<IRepository<Category>>();
            mockRepository.Setup(x => x.GetAll()).Returns(testObjects);

            // Act
            var service = new CategoryService(mockRepository.Object);
            var actual = service.GetAll();

            // Assert
            mockRepository.Verify(x => x.GetAll());
            Assert.Collection(actual,
                item =>
                {
                    Assert.Equal("Name1", item.CategoryName);
                },
                item =>
                {
                    Assert.Equal("Name2", item.CategoryName);
                },
                item =>
                {
                    Assert.Equal("Name3", item.CategoryName);
                });
        }
    }
}
