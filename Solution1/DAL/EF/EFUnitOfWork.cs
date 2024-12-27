using DAL.EF;
using DAL.Repositories.Impl;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private GroupContext db;
        private GroupRepository userRepository;
        private PublicContentRepository publicContentRepository;

        public EFUnitOfWork(GroupContext context)
        {
            db = context;
        }

        public IGroupRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new GroupRepository(db);
                return userRepository;
            }
        }

        public IPublicContentRepository PublicContents
        {
            get
            {
                if (publicContentRepository == null)
                    publicContentRepository = new PublicContentRepository(db);
                return publicContentRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
