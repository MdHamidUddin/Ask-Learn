using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AskNLearn.Models.Entity
{
    public class UsersProfile
    {
       
        public string name { get; set; }
        public int uid { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public System.DateTime dob { get; set; }
        public string gender { get; set; }
        public string proPic { get; set; }
        public string userType { get; set; }
        public System.DateTime dateTime { get; set; }
    }
}