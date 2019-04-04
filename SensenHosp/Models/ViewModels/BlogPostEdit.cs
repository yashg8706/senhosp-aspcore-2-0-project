using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SensenHosp.Models;

namespace SensenHosp.Models.ViewModels
{
    public class BlogPostEdit
    {
        public BlogPostEdit()
        {

        }

        public virtual BlogPost BlogPost { get; set; }

        /*public IEnumerable<BlogPostTag> BlogPostTags { get; set; }

        public IEnumerable<BlogTag> BlogTags { get; set; }*/

        public IEnumerable<BlogCategory> BlogCategories { get; set; }

    }

    
}
