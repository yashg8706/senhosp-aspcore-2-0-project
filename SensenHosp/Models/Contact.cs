using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SensenHosp.Models
{
    public class Contact
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Email Address")]
        public string email { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Phone Number")]
        public string phone { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [StringLength(int.MaxValue)]
        [Display(Name = "Message")]
        public string message { get; set; }

        public DateTime DateSent { get; set; }

        public Boolean message_status { get; set; }
    }
}
