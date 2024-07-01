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
                    courseModel.Price = (decimal)course.Price;
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
        public static List<SubmitAssignmentModel> ConvertSubmissionToSubmissionModelList(List<Submission> submissions)
        {
            try
            {
                List<SubmitAssignmentModel> submitAssignmentModelList = new List<SubmitAssignmentModel>();
                if(submissions != null)
                {
                    foreach(var item in submissions)
                    {
                        SubmitAssignmentModel submitAssignmentModel = new SubmitAssignmentModel();
                        submitAssignmentModel.SubmissionId = item.SubmissionId;
                        submitAssignmentModel.AssignmentId = (int)item.AssignmentId;
                        submitAssignmentModel.UserId = (int)item.UserId;
                        submitAssignmentModel.Grade = item.Grade;
                        submitAssignmentModel.Feedback = item.Feedback;
                        submitAssignmentModel.StudentName = item.Users.Username;
                        submitAssignmentModel.AssignmentTitle = item.Assignment.Title;
                        submitAssignmentModel.AssignmentDescription = item.Assignment.Description;
                        submitAssignmentModelList.Add(submitAssignmentModel);
                    }
                }
                return submitAssignmentModelList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static List<UserModel> ConvertUserListToList(List<Users> users)
        {
            List<UserModel> userModelList = new List<UserModel>();
            foreach(var user in users)
            {
                UserModel userModel = new UserModel
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password,
                    Role = user.Role
                };
                userModelList.Add(userModel);
            }

            return userModelList;
        }
    }

}
