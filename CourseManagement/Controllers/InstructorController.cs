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
    [CustomInstructorAuthorization]
    public class InstructorController : Controller
    {
        private readonly ICourseRepository courseRepository;
        private readonly InstructorRepository instructorRepository;
        public InstructorController()
        {
            courseRepository = new CourseService();
            instructorRepository = new InstructorService();
        }
        public ActionResult Index()
        {
            int Id = SessionHelper.UserId;
            List<CourseModel> courseModelList = courseRepository.GetCourseList(Id);
            return View(courseModelList);
        }
        [HttpGet]
        public ActionResult CreateCourse()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult CreateCourse(CourseModel courseModel)
        {
            if (ModelState.IsValid)
            {
                if(Request.Files.Count > 0)
                {
                    string a = ConvertCourseFileToString(Request.Files[0]);
                    courseModel.Image = a;
                }
                bool CheckSave = courseRepository.AddCourse(courseModel);
                return RedirectToAction("Index");
            }
            return View(courseModel);
        }
        public string ConvertCourseFileToString(HttpPostedFileBase file)
        {
            string uniqefilename = DateTime.Now.ToString("dd-MM-yyyy-ss") + "-" + file.FileName;
            file.SaveAs(HttpContext.Server.MapPath("~/Content/CourseImages/") + uniqefilename);
            return uniqefilename;
        }
        public ActionResult UploadMaterial(int CourseId)
        {
            ViewBag.CourseId = CourseId;
            return View();
        }
        [HttpPost]
        public ActionResult UploadMaterial(MaterialModel materialModel)
        {
            string a = "";
            for (int i = 0; i < Request.Files.Count; i++)
            {
                a += ConvertFileToString(Request.Files[i]);
                if (Request.Files.Count - 1 > i)
                    a += ",";
            }

            materialModel.FilePath = a;

            bool CheckSaveMaterial = instructorRepository.UploadMaterial(materialModel);
            if (CheckSaveMaterial)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(materialModel);
            }
        }

        [NonAction]
        public string ConvertFileToString(HttpPostedFileBase file)
        {
            /*string uniqefilename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);*/
            string uniqefilename = DateTime.Now.ToString("dd-MM-yyyy-ss") + "-" + file.FileName;
            file.SaveAs(HttpContext.Server.MapPath("~/Content/UploadFiles/") + uniqefilename);
            return uniqefilename;
        }

        public ActionResult AssignmentList(int CourseId)
        {
            try
            {
                Session["CourseId"] = CourseId;
                List<AssignmentModel> list = instructorRepository.GetAssignmentModelList(CourseId);

                return View(list);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult CreateAssignment()
        {
            try
            {
                return View();

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult CreateAssignment(AssignmentModel assignmentModel)
        {
            try
            {
                bool CheckSaveAssignment = instructorRepository.CreateAssignment(assignmentModel);
                if (CheckSaveAssignment)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(assignmentModel);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult ReviewAssignment()
        {
            List<SubmitAssignmentModel> list = new List<SubmitAssignmentModel>();
            int UserId = SessionHelper.UserId;
            list = instructorRepository.GetSubmitAssignmentList(UserId);
            return View(list);
        }

        public ActionResult SubmitReview(SubmitAssignmentModel submitReview)
        {
            try
            {
                bool SaveReview = instructorRepository.SubmitReview(submitReview);
                if (SaveReview)
                {
                    TempData["Submit Review"] = "Successfully submit review";
                }
                return RedirectToAction("ReviewAssignment");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult EditCourse(int CourseId)
        {
            CourseModel courseModel = instructorRepository.GetCourseModelById(CourseId);
            return View("CreateCourse",courseModel);
        }

        [HttpPost]
        public ActionResult EditCourse(CourseModel courseModel)
        {
            if(Request.Files.Count > 0  && courseModel.Image != null)
            {
                courseModel.Image = ConvertCourseFileToString(Request.Files[0]);
            }
            bool EditOrNot = instructorRepository.EditCourse(courseModel);
            if (EditOrNot)
            {

                return RedirectToAction("Index");   
            }

            return View("CreateCourse", courseModel);
        }

        public ActionResult DeleteCourse(int CourseId)
        {
            bool delete = instructorRepository.DeleteCourse(CourseId);
            if (delete)
            {
                TempData["deleteCourse"] = "Course Delete Successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult ViewCourse(int CourseId) 
        {
            CourseModel courseModel = instructorRepository.ViewCourse(CourseId);
            return View(courseModel);
        }

        public ActionResult Discussion(int CourseId)
        {
            ViewBag.CourseId = CourseId;
            List<DiscussionModel> list = instructorRepository.DiscussionModelList(CourseId);
            return View(list);
        }

        [HttpPost]
        public ActionResult CreatePost(DiscussionModel discussionModel)
        {
            discussionModel.UserId = SessionHelper.UserId;
            bool ans = instructorRepository.CreatePost(discussionModel);
            return RedirectToAction("Discussion", new { CourseId = discussionModel.CourseId });
        }

        [HttpGet]
        public ActionResult GetComments(int DiscussionId)
        {
            List<CommentModel> list = instructorRepository.CommentList(DiscussionId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}   