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
    public class UsersController : Controller
    {
        // GET: Users
        AskNLearnEntities dbObj = new AskNLearnEntities();
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UsersLoginModel user)
        {
            if (ModelState.IsValid)
            {
                var data = (from u in dbObj.Users
                            where u.username.Equals(user.username) &&
                            u.password.Equals(user.password)
                            select u).FirstOrDefault();

                //User data = dbObj.Users.Where(x => x.username == Model.username && x.password == Model.password).FirstOrDefault();

                var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UsersLoginModel>());

                var mapper = new Mapper(config);

                var Data = mapper.Map<UsersLoginModel>(data);

                if (Data != null)
                {
                    //FormsAuthentication.SetAuthCookie(data.Username, false);
                    //Session["Username"] = data.Username;
                    return RedirectToAction("../Dashboard/Dashboard");
                }
                else if (Data == null)
                {
                    ViewBag.Message = "Your Username Or Password May Be Incorrect";
                }
            }

            return View();
        }


    }
}