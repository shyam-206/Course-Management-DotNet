using CourseManagement.CustomFilter;
using CourseManagement.Session;
using CourseManagement_Model.ViewModel;
using CourseManagement_Repository.Interface;
using CourseManagement_Repository.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseManagement.Controllers
{
    [CustomAuthentication]
    [CustomStudentAuthorization]
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly InstructorRepository instructorRepository;
        public StudentController()
        {
            studentRepository = new StudentService();
            instructorRepository = new InstructorService();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CourseList()
        {
            int UserId = SessionHelper.UserId;
            List<CourseModel> list = studentRepository.GetAllCourseList(UserId);
            return View(list);
        }

        public ActionResult EnrollCourse(int CourseId)
        {
            try
            {
                int UserId = SessionHelper.UserId;
                bool EnrollCourse = studentRepository.EnrollmentCourse(CourseId, UserId);
                if (EnrollCourse)
                {

                    return RedirectToAction("CourseList");
                }
                return RedirectToAction("ErrorPage");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public ActionResult ViewMaterial(int CourseId)
        {
            List<MaterialModel> materialModelList = studentRepository.GetMaterialList(CourseId);

            /* if(materialModel != null && materialModel.MaterialId > 0)
             {
                 materialModel.Files = materialModel.FilePath.Split(',');
                 return View(materialModel);
             }*/

            if (materialModelList != null)
            {
                return View(materialModelList);
            }
            return RedirectToAction("CourseList");

        }

        public FileResult Download(string file)
        {
            string path = Server.MapPath("~/Content/UploadFiles");
            if (file.EndsWith(".pdf"))
            {
                string fullPath = Path.Combine(path, file);
                return File(fullPath, "application/pdf", file);

            }
            else
            {
                string fullPath = Path.Combine(path, file);
                return File(fullPath, "image/jpg", file);

            }
        }

        public ActionResult AssignmentList(int CourseId)
        {
            List<AssignmentModel> assignmentModelList = studentRepository.GetAssignmentModelList(CourseId,SessionHelper.UserId);
            if(assignmentModelList != null)
            {
                return View(assignmentModelList);
            }

            return RedirectToAction("CourseList");       
        }

        public ActionResult SubmitAssignment(int AssignmentId)
        {
            try
            {
                int UserId = SessionHelper.UserId;
                bool SaveAssignment = studentRepository.SubmitAssignment(AssignmentId, UserId);
                if (SaveAssignment)
                {
                    TempData["Save Assignment"] = "Submitted Assignment";
                    return RedirectToAction("CourseList");
                }
                return View("AssignmentList");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}