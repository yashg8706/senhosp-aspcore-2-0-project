using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensenHosp.Models.ViewModels
{
    public class ApplicantEdit
    {
        public ApplicantEdit()
        {

        }

        //To edit a blog, you also need to pick from a list of authors

        public virtual Applicant applicant { get; set; }

        public IEnumerable<Career> careers { get; set; }
    }
}
