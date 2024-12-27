using BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using BLL.DTO;
using DAL.Repositories.Interfaces;
using AutoMapper;
using DAL.UnitOfWork;
using CCL.Security;
using CCL.Security.Identity;

namespace BLL.Services.Impl
{
    public class PublicContentService : IPublicContentService
    {
        private readonly IUnitOfWork _database;
        private int pageSize = 10;

        public PublicContentService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }
            _database = unitOfWork;
        }

        /// <exception cref="MethodAccessException"></exception>
        public IEnumerable<PublicContentDTO> GetPublicContents(int pageNumber)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();

            if (userType != typeof(Admin) && userType != typeof(SpecialServices))
            {
                throw new MethodAccessException();
            }

            var groupId = user.GroupId;
            var publicContentEntities =
                _database
                    .PublicContents
                    .Find(pc => pc.UserID == groupId, pageNumber, pageSize);

            var mapper =
                new MapperConfiguration(cfg => cfg.CreateMap<PublicContent, PublicContentDTO>())
                .CreateMapper();

            var publicContentDto =
                mapper
                    .Map<IEnumerable<PublicContent>, List<PublicContentDTO>>(publicContentEntities);
            return publicContentDto;
        }

        public void AddPublicContent(PublicContentDTO publicContent)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();

            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }

            if (publicContent == null)
            {
                throw new ArgumentNullException(nameof(publicContent));
            }

            Validate(publicContent);

            var mapper =
                new MapperConfiguration(cfg => cfg.CreateMap<PublicContentDTO, PublicContent>())
                .CreateMapper();

            var publicContentEntity =
                mapper.Map<PublicContentDTO, PublicContent>(publicContent);

            _database.PublicContents.Create(publicContentEntity);
        }

        private void Validate(PublicContentDTO publicContent)
        {
            if (string.IsNullOrEmpty(publicContent.PublicContentTitle))
            {
                throw new ArgumentException("PublicContentTitle повинне містити значення!");
            }

            if (string.IsNullOrEmpty(publicContent.PublicContentDescription))
            {
                throw new ArgumentException("PublicContentDescription повинне містити значення!");
            }
        }
    }
}
