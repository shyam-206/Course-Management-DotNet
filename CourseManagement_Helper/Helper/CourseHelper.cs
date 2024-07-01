using CourseManagement_Model.DBContext;
using CourseManagement_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement_Helper.Helper
{
    public class CourseHelper
    {
        public static Course ConvertCourseModelToCourse(CourseModel courseModel)
        {
            try
            {
                Course course = new Course
                {
                    Title = courseModel.Title,
                    Description = courseModel.Description,
                    InstructorId = courseModel.InstructorId,
                    Price = courseModel.Price,
                    CourseImage = courseModel.Image,
                    Created_at = DateTime.Now                    
                };

                return course;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static CourseModel ConvertCourseToCourseModel(Course course)
        {
            try
            {
                CourseModel courseModel = new CourseModel();
                courseModel.CourseId = course.CourseId;
                courseModel.Title = course.Title;
                courseModel.Description = course.Description;
                courseModel.InstructorId = (int)course.InstructorId;
                courseModel.InstructorName = course.Users.Username;
                courseModel.Price = (decimal)course.Price;
                courseModel.Image = course.CourseImage;
                return courseModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static List<CourseModel> ConvertCourseModelListToCourseList(List<Course> courses)
        {
            try
            {
                List<CourseModel> courseModelList = new List<CourseModel>();
                if(courses != null)
                {
                    foreach(var course in courses)
                    {
                        CourseModel courseModel = new CourseModel();
                        courseModel.CourseId = course.CourseId;
                        courseModel.Title = course.Title;
                        courseModel.Description = course.Description;
                        courseModel.InstructorId = (int)course.InstructorId;
                        courseModel.Price = (decimal)course.Price;
                        courseModel.Image = course.CourseImage;
                        courseModelList.Add(courseModel);
                    }
                }
                return courseModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<ReviewModel> ConvertReviewListToList(List<Review> reviews)
        {
            try
            {
                List<ReviewModel> list = new List<ReviewModel>();
                foreach(var review in reviews)
                {
                    ReviewModel reviewModel = new ReviewModel();
                    reviewModel.ReviewId = review.ReviewId;
                    reviewModel.UserId = review.UserId;
                    reviewModel.CourseId = review.CourseId;
                    reviewModel.Rating = review.Rating;
                    reviewModel.ReviewText = review.ReviewText;
                    reviewModel.ReviewDate = review.ReviewDate;
                    reviewModel.StudentName = review.Users.Username;
                    list.Add(reviewModel);
                }

                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
