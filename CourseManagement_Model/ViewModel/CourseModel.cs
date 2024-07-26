using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement_Model.ViewModel
{
    public class CourseModel
    {
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Enter Your Course Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Enter Your Course Description")]
        public string Description { get; set; }
        public int InstructorId { get; set; }
        public bool IsEnrollment { get; set; }
        public string InstructorName { get; set; }
        [Required(ErrorMessage = "Enter Your Course Price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Enter Your Image")]
        public string Image { get; set; }
        public string IsDelete { get; set; }
        public List<ReviewModel> Reviews { get; set; }
        public decimal AvgRating { get; set; }
    }
}
