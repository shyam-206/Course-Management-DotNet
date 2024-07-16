using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement_Model.ViewModel
{
    public class DashboardModel
    {
        public List<string> CourseNameList { get; set; }

        public List<decimal> Rating { get; set; }

        public List<int> EnrollCourseCount { get; set; }

        public int TotalCourseCount { get; set; }
        public int TotalAssignmentCount { get; set; }
        public int TotalCountOfInstructor { get; set; }
        public int TotalCountOfStudent { get; set; }
        public int TotalSubmissionCount { get; set; }
        public int MaterialCount { get; set; }
    }
}
