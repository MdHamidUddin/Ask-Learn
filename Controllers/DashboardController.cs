using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AskNLearn.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Dashboard()
        {

            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult Ratio()
        {
            //int male = dbObj.charts.Where(x => x.Gender == "Male").Count();
            //int female = dbObj.charts.Where(x => x.Gender == "Female").Count();
            //int other = dbObj.charts.Where(x => x.Gender == "Other").Count();
           
            return View();
          
        }
        public ActionResult ShowChart()
        {
            int male = 10;
            int female = 15;
            int other = 5;
            getValue obj = new getValue();
            obj.Male = male;
            obj.Female = female;
            obj.Other = other;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

   

        public class getValue
        {
            public int Male { get; set; }
            public int Female { get; set; }
            public int Other { get; set; }
        }
    }
}