using AskNLearn.Models;
using AskNLearn.Models.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
                var uch = data.username.FirstOrDefault();

                //User data = dbObj.Users.Where(x => x.username == Model.username && x.password == Model.password).FirstOrDefault();

                var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UsersLoginModel>());

                var mapper = new Mapper(config);

                var Data = mapper.Map<UsersLoginModel>(data);
                //'uch' will be the charecter which will define which controller to redirect.
                if (Data != null && uch.Equals('A'))
                {
                        FormsAuthentication.SetAuthCookie(data.username, false);
                        Session["username"] = data.username;
                        return RedirectToAction("../Dashboard/Dashboard");
                }
                else if (Data != null && uch.Equals('I'))
                {
                    FormsAuthentication.SetAuthCookie(data.username, false);
                    Session["username"] = data.username;
                    Session["uid"] = data.uid;
                    return RedirectToAction("../Instructor/Dashboard");
                }
                else if (Data != null && uch.Equals('M'))
                {
                    FormsAuthentication.SetAuthCookie(data.username, false);
                    Session["username"] = data.username;
                    Session["uid"] = data.uid;
                    return RedirectToAction("../gg/Dashboard");
                }
                else if (Data != null && uch.Equals('L'))
                {
                    FormsAuthentication.SetAuthCookie(data.username, false);
                    Session["username"] = data.username;
                    Session["uid"] = data.uid;
                    return RedirectToAction("../gg/Dashboard");
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