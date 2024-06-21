﻿using CourseManagement_Helper.Helper;
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
    public class StudentService : IStudentRepository
    {
        private readonly CourseManagement557Entities _context = new CourseManagement557Entities();

        public bool AddFund(int Amount, int UserId)
        {
            try
            {
                int save = 0;
                Users user = _context.Users.Where(m => m.UserId == UserId).FirstOrDefault();
                if(user != null)
                {
                    user.CreditPrice += Amount;
                    user.Updated_at = DateTime.Now;
                    save  =_context.SaveChanges();
                }
                return save > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EnrollmentCourse(int CourseId, int UserId)
        {
            try
            {
                int CheckEnrollCourse = 0;
                Users user = _context.Users.Where(m => m.UserId == UserId).FirstOrDefault();
                if (user != null && user.UserId > 0 && user.CreditPrice > 0)
                {
                    Enrollment enrollment = new Enrollment
                    {
                        CourseId = CourseId,
                        UserId = UserId,
                        Created_at = DateTime.Now
                    };

                    _context.Enrollment.Add(enrollment);
                    CheckEnrollCourse = _context.SaveChanges();
                }
                return CheckEnrollCourse > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<CourseModel> GetAllCourseList(int UserId)
        {
            try
            {
                List<Course> courses = _context.Course.ToList();
                List<CourseModel> list = CourseHelper.ConvertCourseModelListToCourseList(courses);
                foreach (var item in list)
                {
                    item.IsEnrollment = _context.Enrollment.FirstOrDefault(m => m.CourseId == item.CourseId && m.UserId == UserId) != null ? true : false;
                }
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<UserModel> GetAllStudentList()
        {
            throw new NotImplementedException();
        }

        public decimal GetAmount(int UserId)
        {
            decimal amount = 0; 
            Users user = _context.Users.Where(m => m.UserId == UserId).FirstOrDefault();
            if(user != null)
            {
                amount = (decimal)user.CreditPrice;
            }
            return amount;
        }

        public List<AssignmentModel> GetAssignmentModelList(int CourseId, int UserId)
        {
            try
            {
               
                List<Assignment> list = _context.Assignment.Where(m => m.CourseId == CourseId).ToList();
                List<AssignmentModel>  assignmentModelList = InstructorHelper.ConvertAssignmentListToAssignmentModelList(list);
                foreach(var item in assignmentModelList)
                {
                    /*item.Is_submit = _context.Submission.FirstOrDefault(m => m.AssignmentId == item.AssignmentId && m.UserId == UserId) != null ? true : false;
                    item.Grade = (decimal)_context.Submission.FirstOrDefault(m => m.AssignmentId == item.AssignmentId && m.UserId == UserId).Grade;*/
                    Submission submission = _context.Submission.Where(m => m.AssignmentId == item.AssignmentId && m.UserId == UserId).FirstOrDefault();
                    if(submission != null)
                    {
                        item.Is_submit = true;
                        if(submission.Grade != null && submission.Feedback != null)
                        {
                            item.Grade = (decimal)submission.Grade;
                            item.Feedback = submission.Feedback;
                        }
                        else
                        {
                            item.Grade = 0;
                            item.Feedback = "Review Pending";
                        }
                        
                    }
                    else
                    {
                        item.Is_submit = false;
                    }
                    

                }
                return assignmentModelList != null ? assignmentModelList : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public MaterialModel GetMaterial(int CourseId,int MaterialId)
        {
            try
            {
                Material material = _context.Material.FirstOrDefault(m => m.CourseId == CourseId);
                MaterialModel materialModel = new MaterialModel();
                if (material != null)
                {

                    materialModel.MaterialId = material.MaterialId;
                    materialModel.CourseId = (int)material.CourseId;
                    materialModel.Title = material.Title;
                    materialModel.FilePath = material.FilePath;

                }

                return materialModel != null ? materialModel : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<MaterialModel> GetMaterialList(int CourseId)
        {
            try
            {
                List<Material> list = _context.Material.Where(m => m.CourseId == CourseId).ToList();
                List<MaterialModel> list2 = new List<MaterialModel>();
                if(list != null)
                {
                    list2 = MaterialHelper.ConvertMaterialListToMaterialModelList(list);
                }
                return list2 != null ? list2 : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool SubmitAssignment(int AssignmentId, int UserId)
        {
            try
            {
                int SaveAssignment = 0;
                Assignment assignment = _context.Assignment.Where(m => m.AssignmentId == AssignmentId).FirstOrDefault();
                Submission submission = new Submission();
                if(assignment != null)
                {
                    submission.AssignmentId = assignment.AssignmentId;
                    submission.UserId = UserId;
                    submission.Submitted_at = DateTime.Now;
                    _context.Submission.Add(submission);
                    SaveAssignment = _context.SaveChanges();

                }

                
                return SaveAssignment > 0 ? true : false;

            } 
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdatePurchasePrice(int Amount, int UserId)
        {
            try
            {
                int updateAmount = 0;
                Users user = _context.Users.Where(m => m.UserId == UserId).FirstOrDefault();
                if(user != null)
                {
                    user.CreditPrice -= Amount;
                    user.Updated_at = DateTime.Now;
                    updateAmount = _context.SaveChanges();
                }

                return updateAmount > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
