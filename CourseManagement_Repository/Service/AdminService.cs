using CourseManagement_Helper.Helper;
using CourseManagement_Model.DBContext;
using CourseManagement_Model.ViewModel;
using CourseManagement_Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement_Repository.Service
{
    public class AdminService : IAdminRepository
    {
        private readonly CourseManagement557Entities _context = new CourseManagement557Entities();
        public List<CourseModel> GetALLCourseList()
        {
            List<Course> courses = _context.Course.ToList();
            List<CourseModel> courseModelList = AdminHelper.ConvertCourseToCourseModel(courses);
            return courseModelList;

        }

        public List<AssignmentModel> GetAssignmentList()
        {
            List<Assignment> assignments = _context.Assignment.ToList();
            List<AssignmentModel> assignmentModelList = AdminHelper.ConvertAssignmentListToAssignmentModelList(assignments);
            return assignmentModelList;
        }

        public int GetTotalNumberOfInstructorCount()
        {
            int TotalNumberOfInstructorCount = 0;
            List<Users> users = _context.Users.Where(m => m.Role == "Instructor").ToList();
            TotalNumberOfInstructorCount = users.Count();
            return TotalNumberOfInstructorCount;
        }

        public int GetTotalNumberOfStudentCount()
        {
            int TotalNumberOfStudentCount = 0;
            List<Users> users = _context.Users.Where(m => m.Role == "Student").ToList();
            TotalNumberOfStudentCount = users.Count();
            return TotalNumberOfStudentCount;

        }
    }
}
