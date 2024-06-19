using CourseManagement_Model.DBContext;
using CourseManagement_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement_Helper.Helper
{
    public class InstructorHelper
    {
        public static List<AssignmentModel> ConvertAssignmentListToAssignmentModelList(List<Assignment> list)
        {
            try
            {
                List<AssignmentModel> assignmentModelList = new List<AssignmentModel>();
                if(list != null)
                {
                    foreach(var item in list)
                    {
                        AssignmentModel assignmentModel = new AssignmentModel();
                        assignmentModel.AssignmentId = item.AssignmentId;
                        assignmentModel.CourseId = (int)item.CourseId;
                        assignmentModel.Title = item.Title;
                        assignmentModel.Description = item.Description;
                        assignmentModel.DueDate = (DateTime)item.DueDate;
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

        public static List<SubmitAssignmentModel> ConvertSubmissionListToSubmissionModelList(List<Submission> submissions)
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
                        submitAssignmentModel.StudentName = item.Users.Username;
                        submitAssignmentModel.AssignmentTitle = item.Assignment.Title;
                        submitAssignmentModel.AssignmentDescription = item.Assignment.Description;
                        submitAssignmentModelList.Add(submitAssignmentModel);
                    }
                }
                return submitAssignmentModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
