using CCL.Security.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCL.Security
{
    public static class SecurityContext
    {
        static User _user = null;
        public static User GetUser()
        {
            return _user;
        }
        public static void SetUser(User user)
        {
            _user = user;
        }
    }
}
