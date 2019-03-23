using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SensenHosp.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required, StringLength(255), Display(Name = "First Name")]
        public string UserFName { get; set; }

        [StringLength(255), Display(Name = "Middle Name")]
        public string UserMName { get; set; }

        [Required, StringLength(255), Display(Name = "Last Name")]
        public string UserLName { get; set; }

        [Required, StringLength(255), Display(Name = "Email")]
        public string UserEmail { get; set; }

        [Required, StringLength(20), Display(Name = "Phone")]
        public string UserPhone { get; set; }

        [Display(Name = "DOB")]
        public string UserDOB { get; set; }

        [Required, Display(Name = "CreatedOn")]
        public string UserCreatedOn { get; set; }

        [StringLength(255), Display(Name = "Street Add")]
        public string UserStreetAdd { get; set; }

        [StringLength(255), Display(Name = "City")]
        public string UserCity { get; set; }

        [StringLength(255), Display(Name = "Postal Code")]
        public string UserPostal { get; set; }

        [StringLength(255), Display(Name = "Country")]
        public string UserCountry { get; set; }
    }
}
