using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class PublicContent
    {
        public int PublicContentID { get; set; }
        public string PublicContentTitle { get; set; }
        public string PublicContentDescription { get; set; }

        public int UserID { get; set; }
        public Group User { get; set; }
    }
}
