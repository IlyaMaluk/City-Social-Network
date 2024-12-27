using DAL.EF;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories.Impl
{
    public class PublicContentRepository : BaseRepository<PublicContent>, IPublicContentRepository
    {
        internal PublicContentRepository(GroupContext context) : base(context) 
        {
        }
    }
}
