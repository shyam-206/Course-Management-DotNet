//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CourseManagement_Model.DBContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class Submission
    {
        public int SubmissionId { get; set; }
        public Nullable<int> AssignmentId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<decimal> Grade { get; set; }
        public string Feedback { get; set; }
        public Nullable<System.DateTime> Submitted_at { get; set; }
        public Nullable<System.DateTime> Graded_at { get; set; }
    
        public virtual Assignment Assignment { get; set; }
        public virtual Users Users { get; set; }
    }
}
