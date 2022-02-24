using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AskNLearn.Models.Entity
{
    public class UsersLoginModel
    {
  
        [Required(ErrorMessage ="Enter Your Username")]
        public string username { get; set; }

        [Required(ErrorMessage = "Enter Your Password")]
        public string password { get; set; }
    }
}