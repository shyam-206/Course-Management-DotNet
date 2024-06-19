using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement_Model.ViewModel
{
    public class AssignmentModel
    {
        public int AssignmentId { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Is_submit { get; set; }
        public decimal Grade { get; set; }
        public string Feedback { get; set; }
        public string CourseName { get; set; }
    }
}
