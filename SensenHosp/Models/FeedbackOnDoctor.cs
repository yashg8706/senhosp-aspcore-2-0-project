using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SensenHosp.Models
{
    public class FeedbackOnDoctor
    {
        [Key]
        public int FeedbackID { get; set; }

        [Required,StringLength(500), Display(Name = "Doctor Name")]
        public string DoctorName { get; set; }

        [Required, StringLength(1000), Display(Name = "Feedback")]
        public string Feedback { get; set; }

        [StringLength(1000), Display(Name = "Reply")]
        public string Reply { get; set; }

    }
}
