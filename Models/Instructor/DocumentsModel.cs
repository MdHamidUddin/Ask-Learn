using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AskNLearn.Models.Instructor
{
    public class DocumentsModel
    {
        public int did { get; set; }
        [Required]
        public int coid { get; set; }
        public string image { get; set; }
        public string videoLink { get; set; }
        public string docs { get; set; }
        public string imageTitle { get; set; }
        public string videoTitle { get; set; }
        public string docTitle { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public HttpPostedFileBase DocFile { get; set; }
    }
}