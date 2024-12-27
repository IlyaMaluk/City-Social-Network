using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    public class GroupContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<PublicContent> PublicContents {  get; set; }

        public GroupContext(DbContextOptions options) : base(options)
        {

        }
    }
}
