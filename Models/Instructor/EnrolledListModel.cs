using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AskNLearn.Models.Instructor
{
    public class EnrolledListModel
    {
        public int eid { get; set; }
        public string UserName { get; set; }
        public string courseTitle { get; set; }
        public DateTime dateTime { get; set; }
    }
}