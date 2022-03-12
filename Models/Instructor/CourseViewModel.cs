using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AskNLearn.Models.Instructor
{
    public class CourseViewModel
    {
        public int coid { get; set; }
        public int uid { get; set; }
        public string title { get; set; }
        public string details { get; set; }
        public double price { get; set; }
        public Nullable<int> upVote { get; set; }
        public Nullable<int> downVote { get; set; }
        public System.DateTime dateTime { get; set; }
        public string thumbnail { get; set; }
        //public List<DocumentsModel> Documents { get; set; }
        public string image { get; set; }
        public string videoLink { get; set; }
        public string docs { get; set; }
        public string imageTitle { get; set; }
        public string videoTitle { get; set; }
        public string docTitle { get; set; }
        public int quizId { get; set; }
        public string quizTitle { get; set; }
        public string questionLink { get; set; }
        public string totalMarks { get; set; }
    }
}