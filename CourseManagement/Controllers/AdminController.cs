﻿using CourseManagement.CustomFilter;
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
            ViewBag.TotalSubmissionCount = adminRepository.GetSubmissionCount();
            ViewBag.MaterialCount = adminRepository.GetMaterialCount();
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

        public ActionResult SubmissionList()
        {
            List<SubmitAssignmentModel> submitAssignmentModelList = adminRepository.GetSubmissionModelList();
            return View(submitAssignmentModelList);
        }

        public ActionResult MaterialList()
        {
            List<MaterialModel> list = adminRepository.GetAllMaterial();
            return View(list);
        }

        public FileResult ViewMaterial(string file)
        {
            string path = Server.MapPath("~/Content/UploadFiles");
            if (file.EndsWith(".pdf"))
            {
                string fullPath = Path.Combine(path, file);
                return File(fullPath, "application/pdf");

            }
            else
            {
                string fullPath = Path.Combine(path, file);
                return File(fullPath, "image/jpg");

            }
        }

    }
}