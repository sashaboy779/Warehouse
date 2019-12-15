using Moq;
using DAL;
using Xunit;
using System.Collections.Generic;
using BLL.Services.Implementations;

namespace BLL.Test
{
    public class ServiceTest
    {
        TestClass testObject;

        public ServiceTest()
        {
            testObject = new TestClass { Id = 1, Number = 2 };
        }

        [Fact]
        public void Add_TestObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TestClass>>();
            mockRepository.Setup(x => x.Add(It.IsAny<TestClass>()));

            // Act
            var service = new Service<TestClass>(mockRepository.Object);
            service.Add(testObject);

            // Assert
            mockRepository.Verify(x => x.Add(It.Is<TestClass>(y => y == testObject)));
        }

        [Fact]
        public void Get_TestObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TestClass>>();
            mockRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(testObject);

            // Act
            var service = new Service<TestClass>(mockRepository.Object);
            TestClass actual = service.Get(1);

            // Assert
            mockRepository.Verify(x => x.Get(It.Is<int>(y => y == testObject.Id)));
            Assert.Equal(1, actual.Id);
            Assert.Equal(2, actual.Number);
        }

        [Fact]
        public void Remove_TestObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TestClass>>();
            mockRepository.Setup(x => x.Remove(It.IsAny<int>()));

            // Act
            var service = new Service<TestClass>(mockRepository.Object);
            service.Remove(testObject.Id);

            // Assert
            mockRepository.Verify(x => x.Remove(It.Is<int>(y => y == 1)));
        }

        [Fact]
        public void Update_TestObjectPassed_ProperMethodCalled()
        {
            // Arrange
            TestClass updatedObject = new TestClass { Id = 1, Number = 5 };

            var mockRepository = new Mock<IRepository<TestClass>>();
            mockRepository.Setup(x => x.Update(It.IsAny<TestClass>(), It.IsAny<int>()));

            // Act
            var service = new Service<TestClass>(mockRepository.Object);
            service.Update(updatedObject, updatedObject.Id);

            // Assert
            mockRepository.Verify(x => x.Update(It.Is<TestClass>(t => t == updatedObject), It.Is<int>(n => n == updatedObject.Id)));
        }

        [Fact]
        public void GetAllBy_TestObjectPassed_ProperMethodCalled()
        {
            // Arrange
            List<TestClass> testObjects = new List<TestClass>
            {
                new TestClass{Id = 2, Number = 20},
                new TestClass{Id = 1, Number = 10},
                new TestClass{Id = 0, Number = 30}
            };

            var mockRepository = new Mock<IRepository<TestClass>>();
            mockRepository.Setup(x => x.GetAll()).Returns(testObjects);

            // Act
            var service = new Service<TestClass>(mockRepository.Object);
            var actual = service.GetAllBy("Number");

            // Assert
            mockRepository.Verify(x => x.GetAll());
            Assert.Collection(actual,
                item =>
                {
                    Assert.Equal(1, item.Id);
                    Assert.Equal(10, item.Number);
                },
                item =>
                {
                    Assert.Equal(2, item.Id);
                    Assert.Equal(20, item.Number);
                },
                item =>
                {
                    Assert.Equal(0, item.Id);
                    Assert.Equal(30, item.Number);
                });
        }
    }
}