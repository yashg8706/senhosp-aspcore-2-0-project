using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensenHosp.Models
{
    public class BlogPostTag
    {
        public int PostId { get; set; }
        public BlogPost BlogPost { get; set; }

        public int TagId { get; set; }
        public BlogTag BlogTag { get; set; }
    }
}
