using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensenHosp.Models
{
    public class Career
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Title")]
        public string title { get; set; }

        [Required]
        [StringLength(int.MaxValue)]
        [Display(Name = "Description")]
        public string description { get; set; }

        [Required]
        [Display(Name = "Department")]
        public string department { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string type { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string category { get; set; }

        [Required]
        [Display(Name = "Deadline")]
        public DateTime deadline { get; set; }

        //This is how we can represent a one (career) to many (applicants) relation
        //notice how if we were using a relational database this column
       
        [InverseProperty("Career")]
        public List<Applicant> Applicants { get; set; }
    }
}
