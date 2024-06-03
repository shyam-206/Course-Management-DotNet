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
    public class InstructorService : InstructorRepository
    {
        private readonly CourseManagement557Entities _context = new CourseManagement557Entities();

        public bool CreateAssignment(AssignmentModel assignmentModel)
        {
            try
            {
                int CheckSaveAssignment = 0;
                Assignment assignment = new Assignment
                {
                    CourseId = assignmentModel.CourseId,
                    Title = assignmentModel.Title,
                    Description = assignmentModel.Description,
                    DueDate = assignmentModel.DueDate,
                    Created_at = DateTime.Now
                };

                _context.Assignment.Add(assignment);
                CheckSaveAssignment = _context.SaveChanges();
                return CheckSaveAssignment > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<AssignmentModel> GetAssignmentModelList(int CourseId)
        {
            try
            {
                List<AssignmentModel> assignmentModelList = new List<AssignmentModel>();
                List<Assignment> list = _context.Assignment.Where(m => m.CourseId == CourseId).ToList();
                if(list != null)
                {
                    assignmentModelList = InstructorHelper.ConvertAssignmentListToAssignmentModelList(list);
                }
                return assignmentModelList != null ? assignmentModelList : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool UploadMaterial(MaterialModel materialModel)
        {
            try
            {
                int CheckSaveMaterial = 0;
                Material material = new Material
                {
                    CourseId = materialModel.CourseId,
                    Title = materialModel.Title,
                    FilePath = materialModel.FilePath,
                    Created_at = DateTime.Now
                };

                _context.Material.Add(material);
                CheckSaveMaterial = _context.SaveChanges();
                return CheckSaveMaterial > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
