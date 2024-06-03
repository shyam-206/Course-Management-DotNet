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
    }
}
