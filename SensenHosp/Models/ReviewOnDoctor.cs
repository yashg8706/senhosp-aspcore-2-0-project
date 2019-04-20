using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SensenHosp.Models
{
    public class ReviewOnDoctor
    {
        [Key, ScaffoldColumn(false)]
        public int ReviewId { get; set; }

        [StringLength(100),Display(Name = "Doctor Name")]
        public string DoctorName { get; set; }

        //[ForeignKey("physicianId")]
        [Display(Name = "Doctor Name")]
        public int physicianId { get; set; }
        public virtual Physician Physician { get; set; }

        [Required,StringLength(1000),Display(Name ="Message")]
        public string Message { get; set; }

        [StringLength(1000),Display(Name = "Reply")]
        public string Reply { get; set; }

    }
}
