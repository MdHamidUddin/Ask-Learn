using AskNLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AskNLearn.Controllers
{
    public class HomeController : Controller
    {
        AskNLearnEntities dbObj = new AskNLearnEntities();
        public ActionResult Index()
        {
            int L1 = 0;
            int L2 = 0;
            int L3 = 0;
            int L4 = 0;
            int L5 = 0;

            var users = dbObj.Users.ToList();

            var EnrolledUser = dbObj.EnrolledUsers.ToList();
           
            if(EnrolledUser.Count()<1)
            {
                
            }
           else if (EnrolledUser.Count() < 2)
            {
                L1 = EnrolledUser[0].uid;
            }
            else if (EnrolledUser.Count() < 3)
            {
                L1 = EnrolledUser[0].uid;
                L2 = EnrolledUser[1].uid;

            }
            else if (EnrolledUser.Count() < 4)
            {
                L1 = EnrolledUser[0].uid;
                L2 = EnrolledUser[1].uid;
                L3 = EnrolledUser[2].uid;

            }
            else if (EnrolledUser.Count() < 5)
            {
                L1 = EnrolledUser[0].uid;
                L2 = EnrolledUser[1].uid;
                L3 = EnrolledUser[2].uid;
                L4 = EnrolledUser[3].uid;
            }
            else if (EnrolledUser.Count() < 6)
            {
                L1 = EnrolledUser[0].uid;
                L2 = EnrolledUser[1].uid;
                L3 = EnrolledUser[2].uid;
                L4 = EnrolledUser[3].uid;
                L5 = EnrolledUser[4].uid;
            }
            

                ViewBag.L1 = L1;
                ViewBag.L2 = L2;
                ViewBag.L3 = L3;
                ViewBag.L4 = L4;
                ViewBag.L5 = L5;

            return View(users);
        }

    }
}