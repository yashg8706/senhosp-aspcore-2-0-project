using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SensenHosp.Models
{
    public class Album
    {
        public int ID { get; set; }
        
        [Required]
        [StringLength(50)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public List<Media> Media { get; set; }
    }

}
