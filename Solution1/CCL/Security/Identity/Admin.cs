using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCL.Security.Identity
{
    public class Admin : User
    {
        public Admin(int userId, string username, string name, int groupId) : base(userId, name, groupId, nameof(Admin))
        {

        }
    }
}
