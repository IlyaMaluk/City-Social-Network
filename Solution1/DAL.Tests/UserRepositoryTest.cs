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
        public void GetUsersSortedByEmail_ReturnsUsersSortedByEmail()
        {
            // Arrange
            DbContextOptions options = new DbContextOptionsBuilder<UserContext>()
                .Options;
            var mockContext = new Mock<UserContext>(options);
            var mockDbSet = new Mock<DbSet<User>>();

            var users = new List<User>
            {
                new User { UserId = 1, Name = "Alice", Email = "alice@example.com" },
                new User { UserId = 2, Name = "Bob", Email = "bob@example.com" },
                new User { UserId = 3, Name = "Charlie", Email = "charlie@example.com" }
            }.AsQueryable();

            mockDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            mockContext.Setup(c => c.Set<User>()).Returns(mockDbSet.Object);

            var repository = new UserRepository(mockContext.Object);

            // Act
            var result = repository.GetUsersSortedByEmail();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(users.OrderBy(u => u.Email).ToList(), result.ToList());
        }

        [Fact]
        public void Create_InputUserInstance_CalledAddMethodOfDBSetWithUserInstance()
        {
            // Arrange
            DbContextOptions options = new DbContextOptionsBuilder<UserContext>()
                .Options;
            var mockContext = new Mock<UserContext>(options);
            var mockDbSet = new Mock<DbSet<User>>();
            mockContext.Setup(context => context.Set<User>()).Returns(mockDbSet.Object);

            var repository = new UserRepository(mockContext.Object);
            User expectedUser = new User { UserId = 1, Name = "John Doe", Email = "john@example.com" };

            // Act
            repository.Create(expectedUser);

            // Assert
            mockDbSet.Verify(dbSet => dbSet.Add(expectedUser), Times.Once());
        }
    }
}
