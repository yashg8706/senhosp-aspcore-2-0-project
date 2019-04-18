using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensenHosp.Models
{
    public class Department
    {

        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Department Name")]
        public string name { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Details")]
        public string details { get; set; }

        //blog has author ID
        [ForeignKey("id")]
        public int section_id { get; set; }
        //Blog Author
        public virtual Sections section { get; set; }

    }
}
