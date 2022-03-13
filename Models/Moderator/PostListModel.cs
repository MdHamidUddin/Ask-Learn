using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AskNLearn.Models.Moderator
{
    public class PostListModel
    {
        public int uid { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string userType { get; set; }

        public string approval { get; set; }




        public int pid { get; set; }
        public string title { get; set; }
        public string details { get; set; }
        public double price { get; set; }
        public Nullable<int> upVote { get; set; }
        public Nullable<int> downVote { get; set; }

        public System.DateTime dateTime { get; set; }
    }
}