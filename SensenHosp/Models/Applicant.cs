using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensenHosp.Models
{
    public class Applicant
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "First Name")]
        public string fname { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Last Name")]
        public string lname { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Contact")]
        public string contact { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string type { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string category { get; set; }

        [Required]
        [Display(Name = "Deadline")]
        public DateTime deadline { get; set; }

        //Resume field is compulsary
        public int resume { get; set; }

        [ForeignKey("id")]
        public int career_id { get; set; }
        //Blog Author
        public virtual Career Career { get; set; }
    }
}
