using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement_Model.ViewModel
{
    public class ReviewModel
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public decimal Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }
        public string StudentName { get; set; }
    }
}
