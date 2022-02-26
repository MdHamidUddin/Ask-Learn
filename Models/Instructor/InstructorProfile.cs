using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AskNLearn.Models.Instructor
{
    public class InstructorProfile
    {
        public int uid { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "Name length must not exceed 150")]
        public string name { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public System.DateTime dob { get; set; }
        [Required]
        public string gender { get; set; }
        public string userType { get; set; }
        public string proPic { get; set; }
        public string approval { get; set; }
        public int u_info_id { get; set; }
        public string eduInfo { get; set; }
        public string currentPosition { get; set; }
        public int reputation { get; set; }
        public System.DateTime dateTime { get; set; }
    }
}