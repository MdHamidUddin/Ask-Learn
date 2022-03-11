using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AskNLearn.Models.Entity
{
    public class CoursesModel
    {
        public int coid { get; set; }
        public int uid { get; set; }
        public string title { get; set; }
        public string details { get; set; }
        public double price { get; set; }
        public string thumbnail { get; set; }
        public Nullable<int> upVote { get; set; }
        public Nullable<int> downVote { get; set; }
        public System.DateTime dateTime { get; set; }
    }
}