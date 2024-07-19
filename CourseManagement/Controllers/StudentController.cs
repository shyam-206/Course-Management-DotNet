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
            ViewBag.Balance = studentRepository.GetAmount(UserId);
            return View(list);
        }
        
        public ActionResult EnrollCourse(int CourseId,int Amount)
        {
            try
            {
                int UserId = SessionHelper.UserId;
                bool EnrollCourse = studentRepository.EnrollmentCourse(CourseId, UserId);
                bool UpdateAmount = studentRepository.UpdatePurchasePrice(Amount, UserId);
                if (EnrollCourse && UpdateAmount)
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
            ViewBag.CourseId = CourseId;
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

        public ActionResult AddFund(int Amount)
        {
            bool save = studentRepository.AddFund(Amount, SessionHelper.UserId);
            if (save)
            {
                TempData["AddAmount"] = $"Your Amount {Amount} add successfully";
            }
            return RedirectToAction("CourseList");
        }

        public ActionResult ViewCourse(int CourseId)
        {
            CourseModel courseModel = studentRepository.GetCourseById(CourseId, SessionHelper.UserId);
            return View(courseModel);
        }

        [HttpPost]
        public ActionResult SubmitReview(ReviewModel reviewModel)
        {
            reviewModel.UserId = SessionHelper.UserId;
            bool review = studentRepository.AddReview(reviewModel);
            return RedirectToAction("CourseList");
        }

        public ActionResult Discussion(int CourseId)
        {
            ViewBag.CourseId = CourseId;
            List<DiscussionModel> list = studentRepository.DiscussionModelList(CourseId);
            return View(list);
        }

        [HttpPost]
        public ActionResult Comment(CommentModel commentModel)
        {
            commentModel.UserId = SessionHelper.UserId;
            bool save = studentRepository.AddComment(commentModel);

            if (save)
            {

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet] 
        public ActionResult GetComments(int DiscussionId)
        {
            List<CommentModel> list = studentRepository.CommentList(DiscussionId);
            return Json(list,JsonRequestBehavior.AllowGet);
        }
    }
}