//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AskNLearn.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Comments = new HashSet<Comment>();
            this.Courses = new HashSet<Cours>();
            this.EnrolledUsers = new HashSet<EnrolledUser>();
            this.Posts = new HashSet<Post>();
        }
    
        public int uid { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public System.DateTime dob { get; set; }
        public string gender { get; set; }
        public string userType { get; set; }
        public string occupation { get; set; }
        public string proPic { get; set; }
        public string eduInfo { get; set; }
        public string approval { get; set; }
        public System.DateTime dateTime { get; set; }
    
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Cours> Courses { get; set; }
        public virtual ICollection<EnrolledUser> EnrolledUsers { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
