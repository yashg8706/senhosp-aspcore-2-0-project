using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SensenHosp.Models;

namespace SensenHosp.Models.ViewModels
{
    public class CareerDetails
    {
        public CareerDetails()
        {

        }
        public virtual Career Career { get; set; }

        public IEnumerable<Applicant> Applicants { get; set; }

    }

}
