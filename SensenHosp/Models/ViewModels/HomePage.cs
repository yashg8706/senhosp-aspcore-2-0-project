using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensenHosp.Models.ViewModels
{
    public class HomePage
    {
        public HomePage()
        {

        }

        public IEnumerable<AlertPosts> AlertPosts { get; set; }
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Testimonial> Testimonials { get; set; }
    }

}
