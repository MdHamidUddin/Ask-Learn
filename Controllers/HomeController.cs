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
            var users = dbObj.Users.ToList();

            var EnrolledUser = dbObj.EnrolledUsers.ToList();
            for(int i=0;i<5;i++)
            {
                int L1 = EnrolledUser[0].uid;
                int L2 = EnrolledUser[1].uid;
                int L3 = EnrolledUser[2].uid;
                int L4 = EnrolledUser[3].uid;
                int L5= EnrolledUser[4].uid;

                ViewBag.L1 = L1;
                ViewBag.L2 = L2;
                ViewBag.L3 = L3;
                ViewBag.L4 = L4;
                ViewBag.L5 = L5;

            }

            foreach(var l in EnrolledUser)
            {
                int L1 = l.uid;
            }

            ViewBag.EnrollUser = EnrolledUser;
            return View(users);
        }

    }
}