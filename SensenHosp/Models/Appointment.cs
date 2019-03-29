using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SensenHosp.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display (Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(255)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(255)]
        [Display (Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(255),Display(Name ="Email Id")]
        public string EmailId { get; set; }

        [Required, StringLength(20), Display(Name = "Mobile Number")]
        public string MobileNo { get; set; }

        [Required, StringLength(500), Display(Name = "Description")]
        public string Description { get; set; }

        [Required, StringLength(100), Display(Name = "Doctor's Name")]
        public string DoctorName { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy")]
        [Display(Name = "Appointment Date")]
        public DateTime? AppointmentDate { get; set; }

        [StringLength(20), Display(Name = "Appointment Time")]
        public string AppointmentTime { get; set; }

        public int IsConfirmed { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}
