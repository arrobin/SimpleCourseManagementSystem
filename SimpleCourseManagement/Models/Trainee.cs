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
    using System.ComponentModel.DataAnnotations;

    public partial class Trainee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Trainee()
        {
            this.TraineeCourses = new HashSet<TraineeCourse>();
        }
    
        public int TraineeId { get; set; }
        [Required]
        public string TraineeName { get; set; }
        public string TraineeImage { get; set; }
        [Required]
        public string FatherName { get; set; }
        [Required]
        public string MotherName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public double Age { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string NationalIdCard { get; set; }
        public int UserDetailsId { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
    
        public virtual UserDetail UserDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TraineeCourse> TraineeCourses { get; set; }
    }
}
