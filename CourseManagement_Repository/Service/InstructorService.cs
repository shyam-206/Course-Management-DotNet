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

        public bool EditCourse(CourseModel courseModel)
        {
            try
            {
                int checkEditOrNot = 0;
                Course course = _context.Course.Where(m => m.CourseId == courseModel.CourseId).FirstOrDefault();
                if(course != null)
                {
                    course.CourseId = courseModel.CourseId;
                    course.Title = courseModel.Title;
                    course.Description = courseModel.Description;
                    course.InstructorId = courseModel.InstructorId;
                    course.Updated_at = DateTime.Now;
                    checkEditOrNot =_context.SaveChanges();
                }

                return checkEditOrNot > 0 ? true : false;
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
                List<Assignment> list = _context.Assignment.Where(m => m.CourseId == CourseId).ToList();
                List<AssignmentModel> assignmentModelList = InstructorHelper.ConvertAssignmentListToAssignmentModelList(list);
                return assignmentModelList != null ? assignmentModelList : null;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public CourseModel GetCourseModelById(int CourseId)
        {
            try
            {
                CourseModel courseModel = new CourseModel();
                Course course = _context.Course.Where(m => m.CourseId == CourseId).FirstOrDefault();
                if(course != null)
                {
                    courseModel.CourseId = course.CourseId;
                    courseModel.Title = course.Title;
                    courseModel.Description = course.Description;
                    courseModel.InstructorId = (int)course.InstructorId;
                }

                return courseModel;
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<SubmitAssignmentModel> GetSubmitAssignmentList(int UserId)
        {
            try
            {
                List<Submission> submissions = _context.Submission.Where(m => m.Assignment.Course.InstructorId == UserId).ToList();
                List<SubmitAssignmentModel> list = new List<SubmitAssignmentModel>();
                list = InstructorHelper.ConvertSubmissionListToSubmissionModelList(submissions);
                if(list != null)
                {
                    foreach(var item in list)
                    {
                        Submission submission = _context.Submission.Where(m => m.SubmissionId == item.SubmissionId).FirstOrDefault();
                        if(submission != null && submission.SubmissionId > 0)
                        {
                            item.IsSubmit = true;
                            if (submission.Grade != null && submission.Feedback != null)
                            {
                                item.Grade = submission.Grade;
                                item.Feedback = submission.Feedback;
                            }
                            else
                            {
                                item.Grade = 0;
                                item.Feedback = null;
                            }
                        }
                        else
                        {
                            item.IsSubmit = false;
                        }
                    }
                }
                return list != null ? list : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public bool SubmitReview(SubmitAssignmentModel submitAssignment)
        {
            try
            {
                int saveGrade = 0;
                Submission submission = _context.Submission.Where(m => m.SubmissionId == submitAssignment.SubmissionId).FirstOrDefault();
                submission.Grade = submitAssignment.Grade;
                submission.Feedback = submitAssignment.Feedback;
                submission.Graded_at = DateTime.Now;
                saveGrade = _context.SaveChanges();

                return saveGrade > 0 ? true : false;
            } 
            catch (Exception)
            {

                throw;
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
