using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SensenHosp.Models
{
    public class BlogPost
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Date Published")]
        public DateTime? DatePublished { get; set; }

        public int HasImg { get; set; }
        public string ImgName { get; set; }

        [StringLength(int.MaxValue)]
        [Display(Name = "Body")]
        public string Body { get; set; }

        /*[ForeignKey("BlogCategoryID")]*/
        [Display(Name = "Category")]
        public int BlogCategoryID { get; set; }
        public BlogCategory BlogCategory { get; set; }

        /*[InverseProperty("BlogPost")]
        public List<BlogPostTag> BlogPostsTags { get; set; }*/

        public BlogPost()
        {
            this.DatePublished = DateTime.Today.Date;
        }
    }
}
