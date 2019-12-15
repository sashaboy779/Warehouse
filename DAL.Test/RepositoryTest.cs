using Moq;
using Xunit;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace DAL.Test
{
    public class RepositoryTest
    {
        TestClass testObject;
        List<TestClass> expectedList;
        Mock<DbSet<TestClass>> dbQuerableSetMock;

        public RepositoryTest()
        {
            testObject = new TestClass { Id = 1 };

            expectedList = new List<TestClass>() { testObject };
            dbQuerableSetMock = new Mock<DbSet<TestClass>>();
            dbQuerableSetMock.As<IQueryable<TestClass>>().Setup(x => x.Provider).Returns(expectedList.AsQueryable().Provider);
            dbQuerableSetMock.As<IQueryable<TestClass>>().Setup(x => x.Expression).Returns(expectedList.AsQueryable().Expression);
            dbQuerableSetMock.As<IQueryable<TestClass>>().Setup(x => x.ElementType).Returns(expectedList.AsQueryable().ElementType);
            dbQuerableSetMock.As<IQueryable<TestClass>>().Setup(x => x.GetEnumerator()).Returns(expectedList.AsQueryable().GetEnumerator());
        }

        [Fact]
        public void Add_TestClassObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var context = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<TestClass>>();

            context.Setup(x => x.Set<TestClass>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Add(It.IsAny<TestClass>())).Returns(testObject);

            // Act
            var repository = new Repository<TestClass>(context.Object);
            repository.Add(testObject);

            //Assert
            context.Verify(x => x.Set<TestClass>());
            dbSetMock.Verify(x => x.Add(It.Is<TestClass>(y => y == testObject)));
        }

        [Fact]
        public void Remove_TestClassObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var testObject = new TestClass();

            var context = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<TestClass>>();

            context.Setup(x => x.Set<TestClass>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Find(It.IsAny<int>())).Returns(testObject);
            dbSetMock.Setup(x => x.Remove(It.IsAny<TestClass>())).Returns(testObject);

            // Act
            var repository = new Repository<TestClass>(context.Object);
            repository.Remove(1);

            //Assert
            context.Verify(x => x.Set<TestClass>());
            dbSetMock.Verify(x => x.Find(It.Is<int>(y => y == 1)));
            dbSetMock.Verify(x => x.Remove(It.Is<TestClass>(y => y == testObject)));
        }

        [Fact]
        public void Get_TestClassObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var testObject = new TestClass();

            var context = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<TestClass>>();

            context.Setup(x => x.Set<TestClass>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Find(It.IsAny<int>())).Returns(testObject);

            // Act
            var repository = new Repository<TestClass>(context.Object);
            repository.Get(1);

            // Assert
            context.Verify(x => x.Set<TestClass>());
            dbSetMock.Verify(x => x.Find(It.Is<int>(y => y == 1)));
            //Assert.Equal(2, actual.Id);
        }

        [Fact]
        public void GetAll_TestClassObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var context = new Mock<DbContext>();
            context.Setup(x => x.Set<TestClass>()).Returns(dbQuerableSetMock.Object);

            // Act
            var repository = new Repository<TestClass>(context.Object);
            var result = repository.GetAll();

            // Assert
            Assert.Equal(expectedList, result.ToList());
        }

        [Fact]
        public void Find_TestClassObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var context = new Mock<DbContext>();
            context.Setup(x => x.Set<TestClass>()).Returns(dbQuerableSetMock.Object);

            // Act
            var repository = new Repository<TestClass>(context.Object);
            var actual = repository.Find(x => x.Id == 1);

            Assert.Equal(expectedList, actual.ToList());
        }
    }
}
