using Moq;
using DAL;
using Xunit;
using Entities;
using System.Collections.Generic;
using BLL.Services.Implementations;

namespace BLL.Test
{
    public class CategoryServiceTest
    {
        [Fact]
        public void GetAll_CategoriesPassed_ProperMethodCalled()
        {
            // Arrange
            List<Category> categories = new List<Category>
            {
                new Category{ CategoryName = "Name1"},
                new Category{ CategoryName = "Name2"},
                new Category{ CategoryName = "Name3"}
            };

            var mockRepository = new Mock<IRepository<Category>>();
            mockRepository.Setup(x => x.GetAll()).Returns(categories);

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
