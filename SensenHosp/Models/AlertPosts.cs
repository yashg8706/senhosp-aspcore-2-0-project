using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SensenHosp.Models
{
    public class AlertPosts
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Alert Title")]
        public string AlertTitle { get; set; }

        [StringLength(int.MaxValue)]
        [Display(Name = "Alert Description")]
        public string Description { get; set; }

        [StringLength(255)]
        [Display(Name = "Alert Status")]
        public string AlertStatus { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateEffectivity { get; set; }
    }
}
