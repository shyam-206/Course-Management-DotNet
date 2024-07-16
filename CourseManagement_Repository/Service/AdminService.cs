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
        private readonly CourseManagement557Entities1 _context = new CourseManagement557Entities1();

        public List<MaterialModel> GetAllMaterial()
        {
            List<Material> materials = _context.Material.ToList();
            List<MaterialModel> materialModelList = new List<MaterialModel>();
            materialModelList = MaterialHelper.ConvertMaterialListToMaterialModelList(materials);
            return materialModelList;
        }

        public List<CourseModel> GetALLCourseList()
        {
            List<Course> courses = _context.Course.Where(m => m.IsDelete != true).ToList();
            List<CourseModel> courseModelList = AdminHelper.ConvertCourseToCourseModel(courses);
            return courseModelList;
        }

        public List<EnrollmentModel> GetAllEnrollmentList()
        {
            throw new NotImplementedException();
        }

        public List<AssignmentModel> GetAssignmentList()
        {
            List<Assignment> assignments = _context.Assignment.ToList();
            List<AssignmentModel> assignmentModelList = AdminHelper.ConvertAssignmentListToAssignmentModelList(assignments);
            return assignmentModelList;
        }

        public int GetSubmissionCount()
        {
            try
            {
                int SubmissionCount = 0;
                List<Submission> submissions = _context.Submission.ToList();
                SubmissionCount = submissions.Count();
                return SubmissionCount;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<SubmitAssignmentModel> GetSubmissionModelList()
        {
            List<Submission> submissions = _context.Submission.ToList();
            List<SubmitAssignmentModel> submitAssignmentModelList = AdminHelper.ConvertSubmissionToSubmissionModelList(submissions);
            foreach (var item in submitAssignmentModelList)
            {
                Assignment assignment = _context.Assignment.Where(m => m.AssignmentId == item.AssignmentId).FirstOrDefault();
                item.InstructorName = assignment.Course.Users.Username;
                item.CourseTitle = assignment.Course.Title;
            }
            return submitAssignmentModelList;
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

        public int GetMaterialCount()
        {
            int MaterialCount = _context.Material.ToList().Count();
            return MaterialCount;
        }

        public List<UserModel> InstructorList()
        {
            try
            {
                List<Users> users = _context.Users.Where(m => m.Role == "Instructor").ToList();
                List<UserModel> InstructorList = AdminHelper.ConvertUserListToList(users);
                return InstructorList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<UserModel> StudentList()
        {
            List<Users> users = _context.Users.Where(m => m.Role == "Student").ToList();
            List<UserModel> StudentList = AdminHelper.ConvertUserListToList(users);
            return StudentList;
        }
        public decimal CalcAvarageRating(List<Review> reviews)
        {
            decimal avg = 0;
            if (reviews != null && reviews.Count() > 0)
            {
                decimal count = reviews.Count();
                foreach (var item in reviews)
                {
                    avg += item.Rating;
                }

                return (avg / count);
            }

            return avg;
        }
        public DashboardModel GetDashboardService()
        {
            DashboardModel dashboardModel = new DashboardModel();
            List<Course> courses = _context.Course.Where(m => m.IsDelete != true).ToList();
            List<string> CourseNameList = new List<string>();
            List<int> EnrollCourseCount = new List<int>();
            List<decimal> Rating = new List<decimal>();
            foreach (var item in courses)
            {
                string CourseName = item.Title;
                int EnrollCount = _context.Enrollment.Where(m => m.CourseId == item.CourseId).Count();
                decimal AvgRating = CalcAvarageRating(_context.Review.Where(m => m.CourseId == item.CourseId).ToList());
                CourseNameList.Add(CourseName);
                EnrollCourseCount.Add(EnrollCount);
                Rating.Add(AvgRating);
            }

            dashboardModel.CourseNameList = CourseNameList;
            dashboardModel.EnrollCourseCount = EnrollCourseCount;
            dashboardModel.Rating = Rating;
            dashboardModel.TotalCourseCount = courses.Count();
            dashboardModel.TotalAssignmentCount = GetAssignmentList().Count();
            dashboardModel.TotalCountOfInstructor = GetTotalNumberOfInstructorCount();
            dashboardModel.TotalCountOfStudent = GetTotalNumberOfStudentCount();
            dashboardModel.TotalSubmissionCount = GetSubmissionCount();
            dashboardModel.MaterialCount = GetMaterialCount();


            return dashboardModel;
        }
    }
}
