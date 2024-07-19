using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement_Model.ViewModel
{
    public class CommentModel
    {
        public int CommentId { get; set; }
        public int DiscussionId { get; set; }
        public string Content { get; set; }
        public DateTime Created_at { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }

        public string Formatdate => Created_at.ToString("dd/MM/yyyy");
    }
}
