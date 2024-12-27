using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCL.Security.Identity
{
    public class SpecialServices : User
    {
        public SpecialServices(int userId, string name, int groupId) : base(userId, name, groupId, nameof(SpecialServices)) 
        {

        }
    }
}
