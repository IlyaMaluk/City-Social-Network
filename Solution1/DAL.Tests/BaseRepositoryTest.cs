using System;
using Xunit;
using DAL.Repositories.Impl;
using DAL.EF;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using System.Linq;
using Moq;
using System.IO;

namespace DAL.Tests
{
    class TestPublicContentRepository : BaseRepository<PublicContent>
    {
        public TestPublicContentRepository(DbContext context) : base(context)
        {
        }
    }

    public class BaseRepositoryUnitTests
    {

        [Fact]
        public void Create_InputPublicContentInstance_CalledAddMethodOfDBSetWithPublicContentInstance()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<UserContext>()
                .Options;
            var mockContext = new Mock<UserContext>(opt);
            var mockDbSet = new Mock<DbSet<PublicContent>>();
            mockContext
                .Setup(context =>
                    context.Set<PublicContent>(
                        ))
                .Returns(mockDbSet.Object);
            //EFUnitOfWork uow = new EFUnitOfWork(mockContext.Object);
            var repository = new TestPublicContentRepository(mockContext.Object);

            PublicContent expectedPublicContent = new Mock<PublicContent>().Object;

            //Act
            repository.Create(expectedPublicContent);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Add(
                    expectedPublicContent
                    ), Times.Once());
        }

        [Fact]
        public void Delete_InputId_CalledFindAndRemoveMethodsOfDBSetWithCorrectArg()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<UserContext>()
                .Options;
            var mockContext = new Mock<UserContext>(opt);
            var mockDbSet = new Mock<DbSet<PublicContent>>();
            mockContext
                .Setup(context =>
                    context.Set<PublicContent>(
                        ))
                .Returns(mockDbSet.Object);
            var repository = new TestPublicContentRepository(mockContext.Object);

            PublicContent expectedPublicContent = new PublicContent() { PublicContentID = 1 };
            mockDbSet.Setup(mock => mock.Find(expectedPublicContent.PublicContentID)).Returns(expectedPublicContent);

            //Act
            repository.Delete(expectedPublicContent.PublicContentID);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedPublicContent.PublicContentID
                    ), Times.Once());
            mockDbSet.Verify(
                dbSet => dbSet.Remove(
                    expectedPublicContent
                    ), Times.Once());
        }

        [Fact]
        public void Get_InputId_CalledFindMethodOfDBSetWithCorrectId()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<UserContext>()
                .Options;
            var mockContext = new Mock<UserContext>(opt);
            var mockDbSet = new Mock<DbSet<PublicContent>>();
            mockContext
                .Setup(context =>
                    context.Set<PublicContent>(
                        ))
                .Returns(mockDbSet.Object);

            PublicContent expectedPublicContent = new PublicContent() { PublicContentID = 1 };
            mockDbSet.Setup(mock => mock.Find(expectedPublicContent.PublicContentID))
                    .Returns(expectedPublicContent);
            var repository = new TestPublicContentRepository(mockContext.Object);

            //Act
            var actualPublicContent = repository.Get(expectedPublicContent.PublicContentID);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedPublicContent.PublicContentID
                    ), Times.Once());
            Assert.Equal(expectedPublicContent, actualPublicContent);
        }


    }
}