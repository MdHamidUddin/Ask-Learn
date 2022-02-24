using AskNLearn.Models;
using AskNLearn.Models.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AskNLearn.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Dashboard
        AskNLearnEntities dbObj = new AskNLearnEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Dashboard()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Profile()
        {
            // User data = dbObj.Users.Where(x => x.username == Session["Username"]).FirstOrDefault();
            var user = (string)Session["Username"];
            var data = (from u in dbObj.Users
                        where u.username.Equals(user)
                        select u).FirstOrDefault();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UsersProfile>());

            var mapper = new Mapper(config);

            var Data = mapper.Map<UsersProfile>(data);


            return View(data);
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