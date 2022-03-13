using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AskNLearn.Models.Instructor
{
    public class PostModel
    {
        public int pid { get; set; }
        public int PostedbyUid { get; set; }
        public string title { get; set; }
        public string details { get; set; }
        public string Postedby { get; set; }
        public Nullable<int> upVote { get; set; }
        public Nullable<int> downVote { get; set; }
        public System.DateTime dateTime { get; set; }
    }
}