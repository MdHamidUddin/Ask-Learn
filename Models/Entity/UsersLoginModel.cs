using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AskNLearn.Models.Entity
{
    public class UsersLoginModel
    {
  
        [Required(ErrorMessage ="Username Cannot Be Empty")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password Cannot Be Empty")]
        public string password { get; set; }
    }
}