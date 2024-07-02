using CourseManagement_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement_Repository.Interface
{
    public interface InstructorRepository
    {
        bool UploadMaterial(MaterialModel materialModel);
        bool CreateAssignment(AssignmentModel assignmentModel);
        List<AssignmentModel> GetAssignmentModelList(int CourseId);
        List<SubmitAssignmentModel> GetSubmitAssignmentList(int UserId);
        bool SubmitReview(SubmitAssignmentModel submitAssignment);

        CourseModel GetCourseModelById(int CourseId);
        bool EditCourse(CourseModel courseModel);

        bool DeleteCourse(int CourseId);

        CourseModel ViewCourse(int CourseId);
    }
}
