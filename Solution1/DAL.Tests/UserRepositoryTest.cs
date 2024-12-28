using System;
using Xunit;
using Moq;
using DAL.Repositories.Impl;
using DAL.EF;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Tests
{
    public class UserRepositoryUnitTests
    {
        [Fact]
        public void GetGroupsSortedByEmail_ReturnsGroupsSortedByEmail()
        {
            // Arrange
            DbContextOptions options = new DbContextOptionsBuilder<GroupContext>()
                .Options;
            var mockContext = new Mock<GroupContext>(options);
            var mockDbSet = new Mock<DbSet<Group>>();

            var users = new List<Group>
            {
                new Group { GroupId = 1, Name = "Alice", Email = "alice@example.com" },
                new Group { GroupId = 2, Name = "Bob", Email = "bob@example.com" },
                new Group { GroupId = 3, Name = "Charlie", Email = "charlie@example.com" }
            }.AsQueryable();

            mockDbSet.As<IQueryable<Group>>().Setup(m => m.Provider).Returns(users.Provider);
            mockDbSet.As<IQueryable<Group>>().Setup(m => m.Expression).Returns(users.Expression);
            mockDbSet.As<IQueryable<Group>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockDbSet.As<IQueryable<Group>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            mockContext.Setup(c => c.Set<Group>()).Returns(mockDbSet.Object);

            var repository = new GroupRepository(mockContext.Object);

            // Act
            var result = repository.GetGroupsSortedByEmail();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(users.OrderBy(u => u.Email).ToList(), result.ToList());
        }

        [Fact]
        public void Create_InputGroupInstance_CalledAddMethodOfDBSetWithGroupInstance()
        {
            // Arrange
            DbContextOptions options = new DbContextOptionsBuilder<GroupContext>()
                .Options;
            var mockContext = new Mock<GroupContext>(options);
            var mockDbSet = new Mock<DbSet<Group>>();
            mockContext.Setup(context => context.Set<Group>()).Returns(mockDbSet.Object);

            var repository = new GroupRepository(mockContext.Object);
            Group expectedUser = new Group { GroupId = 1, Name = "John Doe", Email = "john@example.com" };

            // Act
            repository.Create(expectedUser);

            // Assert
            mockDbSet.Verify(dbSet => dbSet.Add(expectedUser), Times.Once());
        }

        [Fact]
        public void Delete_InputId_CalledFindAndRemoveMethodsOfDBSetWithCorrectArg()
        {
            // Arrange
            DbContextOptions options = new DbContextOptionsBuilder<GroupContext>()
                .Options;
            var mockContext = new Mock<GroupContext>(options);
            var mockDbSet = new Mock<DbSet<Group>>();
            mockContext.Setup(context => context.Set<Group>()).Returns(mockDbSet.Object);

            var repository = new GroupRepository(mockContext.Object);

            Group expectedUser = new Group { GroupId = 1, Name = "John Doe", Email = "john@example.com" };
            mockDbSet.Setup(mock => mock.Find(expectedUser.GroupId)).Returns(expectedUser);

            // Act
            repository.Delete(expectedUser.GroupId);

            // Assert
            mockDbSet.Verify(dbSet => dbSet.Find(expectedUser.GroupId), Times.Once());
            mockDbSet.Verify(dbSet => dbSet.Remove(expectedUser), Times.Once());
        }

    }
}
