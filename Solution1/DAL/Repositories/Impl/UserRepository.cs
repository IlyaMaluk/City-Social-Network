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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(UserContext context) : base(context) 
        { 
        }

        public IEnumerable<User> GetUsersSortedByEmail()
        {
            return _context.Set<User>().OrderBy(u => u.Email).ToList();
        }

    }
}
