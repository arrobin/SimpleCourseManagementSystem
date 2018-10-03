//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SimpleCourseManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Trainee
    {
        public int TraineeId { get; set; }
        public string TraineeCode { get; set; }
        public int BatchId { get; set; }
        public string TraineeName { get; set; }
        public string TraineeImage { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Gender { get; set; }
        public double Age { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string NationalIdCard { get; set; }
        public string Result { get; set; }
        public int UserDetailsId { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
    
        public virtual Batch Batch { get; set; }
        public virtual UserDetail UserDetail { get; set; }
    }
}