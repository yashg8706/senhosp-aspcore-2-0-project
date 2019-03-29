using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SensenHosp.Models
{
    public class FreqAskQuestion
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Question")]
        public string Question { get; set; }

        [StringLength(int.MaxValue)]
        [Display(Name = "Answer")]
        public string Answer { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
