using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SensenHosp.Models
{
    public class UserRole
    {
        [Key]
        public int RoleID { get; set; }

        [Required, StringLength(50), Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [StringLength(1000), Display(Name = "Role Description")]
        public string RoleDescription { get; set; }
    }
}
