using CourseManagement.CustomFilter;
using CourseManagement_Model.ViewModel;
using CourseManagement_Repository.Interface;
using CourseManagement_Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseManagement.Controllers
{
    [CustomAuthentication]
    [CustomAdminAuthorization]
    public class AdminController : Controller
    {
        private readonly IAdminRepository adminRepository = new AdminService();
        public ActionResult Index()
        {
            List<CourseModel> list = adminRepository.GetALLCourseList();
            List<AssignmentModel> assignmentList = adminRepository.GetAssignmentList();
            ViewBag.TotalCourseCount = list.Count();
            ViewBag.TotalAssignmentCount = assignmentList.Count();
            ViewBag.TotalCountOfInstrctor = adminRepository.GetTotalNumberOfInstructorCount();
            ViewBag.TotalCountOfStudent = adminRepository.GetTotalNumberOfStudentCount();
            return View();
        }

        public ActionResult CourseList()
        {
            List<CourseModel> list = adminRepository.GetALLCourseList();
            return View(list);
        }

        public ActionResult AssignmentList()
        {
            List<AssignmentModel> assignmentList = adminRepository.GetAssignmentList();
            return View(assignmentList);
        }

    }
}