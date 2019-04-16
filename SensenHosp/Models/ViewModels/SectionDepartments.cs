using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensenHosp.Models.ViewModels
{
    public class SectionDepartments
    {
        public SectionDepartments()
        {

        }

        //To edit a blog, you also need to pick from a list of authors

        public virtual Department department { get; set; }

        public IEnumerable<Sections> sections { get; set; }
    }
}
