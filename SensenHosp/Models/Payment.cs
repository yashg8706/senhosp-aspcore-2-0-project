using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensenHosp.Models
{
    public class Payment
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "First Name")]
        public string patient_fname { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Last Name")]
        public string patient_lname { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Invoice number")]
        public string invoice_id { get; set; }

        [Required]
        [Display(Name = "Invoice Date")]
        public string invoice_date { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Transaction amount")]
        public string transaction_amount { get; set; }

        [Required]
        //[StringLength(100)]
        //[Display(Name = "Transaction number")]
        public string transaction_number { get; set; }

        //[ForeignKey("id")]
        //public int patient_id { get; set; }
    }
}
