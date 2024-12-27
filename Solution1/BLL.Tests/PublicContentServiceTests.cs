using BLL.Services.Impl;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;
using CCL.Security;
using CCL.Security.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using BLL.DTO;

namespace BLL.Tests
{
    public class PublicContentServiceTests
    {
        [Fact]
        public void Ctor_InputNull_ThrowArgumentNullException()
        {
            // Arrange
            IUnitOfWork nullUnitOfWork = null;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new PublicContentService(nullUnitOfWork));
        }

        [Fact]
        public void GetPublicContents_UserIsNotAdminOrSpecialServices_ThrowMethodAccessException()
        {
            // Arrange
            User user = new Guest(1, "Test Guest", 1);
            SecurityContext.SetUser(user);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            IPublicContentService publicContentService = new PublicContentService(mockUnitOfWork.Object);

            // Act
            // Assert
            Assert.Throws<MethodAccessException>(() => publicContentService.GetPublicContents(0));
        }

        [Fact]
        public void GetPublicContents_ContentFromDAL_CorrectMappingToPublicContentDTO()
        {
            // Arrange
            User user = new Admin(1, "Test Admin", "Test Group", 1);
            SecurityContext.SetUser(user);
            var publicContentService = GetPublicContentService();

            // Act
            var actualContentDto = publicContentService.GetPublicContents(0).First();

            // Assert
            Assert.True(
                actualContentDto.PublicContentID == 1 &&
                actualContentDto.PublicContentTitle == "Test Title" &&
                actualContentDto.PublicContentDescription == "Test Description"
            );
        }

        [Fact]
        public void AddPublicContent_UserIsNotAdmin_ThrowMethodAccessException()
        {
            // Arrange
            User user = new Guest(1, "Test Guest", 1);
            SecurityContext.SetUser(user);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            IPublicContentService publicContentService = new PublicContentService(mockUnitOfWork.Object);

            var publicContentDTO = new PublicContentDTO
            {
                PublicContentID = 1,
                PublicContentTitle = "Title",
                PublicContentDescription = "Description"
            };

            // Act
            // Assert
            Assert.Throws<MethodAccessException>(() => publicContentService.AddPublicContent(publicContentDTO));
        }

        [Fact]
        public void AddPublicContent_ValidInput_CallsCreateMethodOfRepository()
        {
            // Arrange
            User user = new Admin(1, "Test Admin", "Test Group", 1);
            SecurityContext.SetUser(user);

            var mockPublicContentRepo = new Mock<IPublicContentRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.PublicContents).Returns(mockPublicContentRepo.Object);

            IPublicContentService publicContentService = new PublicContentService(mockUnitOfWork.Object);

            var publicContentDTO = new PublicContentDTO
            {
                PublicContentID = 1,
                PublicContentTitle = "Test Title",
                PublicContentDescription = "Test Description"
            };

            // Act
            publicContentService.AddPublicContent(publicContentDTO);

            // Assert
            mockPublicContentRepo.Verify(r => r.Create(It.IsAny<PublicContent>()), Times.Once);
        }

        IPublicContentService GetPublicContentService()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var expectedContent = new PublicContent
            {
                PublicContentID = 1,
                PublicContentTitle = "Test Title",
                PublicContentDescription = "Test Description",
                UserID = 1
            };

            var mockRepo = new Mock<IPublicContentRepository>();
            mockRepo.Setup(repo =>
                repo.Find(
                    It.IsAny<Func<PublicContent, bool>>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .Returns(new List<PublicContent> { expectedContent });

            mockUnitOfWork.Setup(uow => uow.PublicContents).Returns(mockRepo.Object);

            return new PublicContentService(mockUnitOfWork.Object);
        }
    }
}
