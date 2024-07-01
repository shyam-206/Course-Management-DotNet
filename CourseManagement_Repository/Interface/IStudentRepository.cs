using CourseManagement_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement_Repository.Interface
{
    public interface IStudentRepository
    {
        bool EnrollmentCourse(int CourseId,int UserId);
        List<CourseModel> GetAllCourseList(int UserId);
        List<MaterialModel> GetMaterialList(int CourseId);
        MaterialModel GetMaterial(int CourseId,int MaterialId);
        List<UserModel> GetAllStudentList();
        List<AssignmentModel> GetAssignmentModelList(int CourseId,int UserId);
        bool SubmitAssignment(int AssignmentId, int UserId);
        decimal GetAmount(int UserId);
        bool AddFund(int Amount, int UserId);
        bool UpdatePurchasePrice(int Amount, int UserId);
        CourseModel GetCourseById(int CourseId,int UserId);

        bool AddReview(ReviewModel reviewModel);
    }
}
