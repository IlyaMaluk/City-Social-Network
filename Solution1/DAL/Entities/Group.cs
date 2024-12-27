using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        private string Password { get; set; }

        public List<PublicContent> PublicContents { get; set; }
    }
}
