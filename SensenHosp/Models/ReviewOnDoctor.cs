using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SensenHosp.Models
{
    public class ReviewOnDoctor
    {
        [Key]
        public int ReviewId { get; set; }

        [Required,StringLength(100),Display(Name = "Doctor Name")]
        public string DoctorName { get; set; }

        [Required,StringLength(1000),Display(Name ="Message")]
        public string Message { get; set; }

        [StringLength(1000),Display(Name = "Reply")]
        public string Reply { get; set; }

    }
}
