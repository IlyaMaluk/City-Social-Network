using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCL.Security.Identity
{
    public abstract class User
    {
        public User(int userId, string name, int groupId, string userType) 
        {
            UserId = userId;
            Name = name;
            GroupId = groupId;
            UserType = userType;
        }
        public int UserId { get; }
        public string Name { get; }
        public int GroupId { get; }
        public string UserType { get; }
    }
}
