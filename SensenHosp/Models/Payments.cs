using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensenHosp.Models
{
    public class Payments
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Patient First Name")]
        public string patientFname { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Patient last Name")]
        public string patientLname { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Invoice ID")]
        public string invoiceId { get; set; }

        [Required]
        [Display(Name = "Invoice Date")]
        public DateTime invoiceDate { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public string amount { get; set; }

        public string payeeEmail { get; set; }

        public string transactionId { get; set; }
    }
}
