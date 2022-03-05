using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AskNLearn.Models.Instructor
{
    public class DocumentsModel
    {
        public int did { get; set; }
        public int coid { get; set; }
        public string image { get; set; }
        public string videoLink { get; set; }
        public string docs { get; set; }
    }
}