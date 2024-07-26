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
    public class LoginService : ILoginRepository
    {
        private readonly CourseManagement557Entities1 _context = new CourseManagement557Entities1();
        public bool AddUser(UserModel userModel)
        {
            Users user = new Users();
            user = LoginHelper.ConvertUserModelToUser(userModel);
            int CheckUserSave = 0;

            _context.Users.Add(user);
            CheckUserSave = _context.SaveChanges();

           
            return CheckUserSave > 0 ? true : false;
        }

        public UserModel CheckUserExist(LoginModel loginModel)
        {
            try
            {
                bool Exist;
                Exist = _context.Users.Any(m => m.Email == loginModel.Email && m.Password == loginModel.Password);
                UserModel userModel = new UserModel();
                if(Exist)
                {
                    Users user = _context.Users.Where(m => m.Email == loginModel.Email && m.Password == loginModel.Password).FirstOrDefault();
                    userModel = LoginHelper.ConvertUserToUserModel(user);
                }
                return userModel != null ? userModel : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
