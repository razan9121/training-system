//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SummerTraining.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Regestered_In_The_Course
    {
        public int User_Id { get; set; }
        public int Course_Id { get; set; }
        public string Attendance_Status { get; set; }
        public string Evaluation_Status { get; set; }
        public string Certificate_Status { get; set; }
    
        public virtual Course_Schedule Course_Schedule { get; set; }
        public virtual User User { get; set; }
    }
}
