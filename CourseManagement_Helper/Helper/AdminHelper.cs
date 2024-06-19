using CourseManagement_Model.DBContext;
using CourseManagement_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement_Helper.Helper
{
    public class AdminHelper
    {
        public static List<CourseModel> ConvertCourseToCourseModel(List<Course> courses)
        {
            List<CourseModel> courseModelList = new List<CourseModel>();
            if (courses != null)
            {
                foreach (var course in courses)
                {
                    CourseModel courseModel = new CourseModel();
                    courseModel.CourseId = course.CourseId;
                    courseModel.Title = course.Title;
                    courseModel.Description = course.Description;
                    courseModel.InstructorId = (int)course.InstructorId;
                    courseModel.InstructorName = course.Users.Username;
                    courseModelList.Add(courseModel);
                }
            }
            return courseModelList;
        }
        public static List<AssignmentModel> ConvertAssignmentListToAssignmentModelList(List<Assignment> assignments)
    {
        try
        {
            List<AssignmentModel> assignmentModelList = new List<AssignmentModel>();
            if (assignments != null)
            {
                foreach (var item in assignments)
                {
                    AssignmentModel assignmentModel = new AssignmentModel();
                    assignmentModel.AssignmentId = item.AssignmentId;
                    assignmentModel.CourseId = (int)item.CourseId;
                    assignmentModel.Title = item.Title;
                    assignmentModel.Description = item.Description;
                    assignmentModel.DueDate = (DateTime)item.DueDate;
                    assignmentModel.CourseName = item.Course.Title;
                    assignmentModelList.Add(assignmentModel);
                }
            }
            return assignmentModelList;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    }

}
