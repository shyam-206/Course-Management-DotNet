using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement_Model.ViewModel
{
    public class SubmitAssignmentModel
    {
        public int SubmissionId { get; set; }
        public int AssignmentId { get; set; }
        public int UserId { get; set; }
        public Nullable<decimal> Grade { get; set; }
        public string Feedback { get; set; }
        public string AssignmentTitle { get; set; }
        public string AssignmentDescription { get; set; }
        public string StudentName { get; set; }
        public string CourseTitle { get; set; }

        public string InstructorName { get; set; }

        public bool IsSubmit { get; set; }

    }
}
