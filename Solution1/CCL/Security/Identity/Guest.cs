using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCL.Security.Identity
{
    public class Guest : User
    {
        public Guest(int userId, string name, int groupId) : base(userId, name, groupId, nameof(Guest))
        { 
        }
    }
}
