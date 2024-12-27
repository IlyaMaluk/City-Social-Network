using DAL.EF;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Impl
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public GroupRepository(UserContext context) : base(context) 
        { 
        }

        public IEnumerable<Group> GetUsersSortedByEmail()
        {
            return _context.Set<Group>().OrderBy(u => u.Email).ToList();
        }

    }
}
