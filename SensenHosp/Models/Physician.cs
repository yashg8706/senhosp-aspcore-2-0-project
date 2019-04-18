using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SensenHosp.Models
{
    public class Physician
    {
        
        [Key, ScaffoldColumn(false)]
        public int physicianId { get; set; }

        [Required, StringLength(100), Display(Name = "Physician Name")]
        public string physicianName { get; set; }

        [Required, StringLength(100), Display(Name = "Start Time")]
        public string scheduleStartTime { get; set; }

        [Required, StringLength(100), Display(Name = "End Time")]
        public string scheduleEndTime { get; set; }
    }
}
