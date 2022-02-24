using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AskNLearn.Controllers
{
    [Authorize]
    public class InstructorController : Controller
    {
        // GET: Instructor
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Profile()
        {
            return View();
        }
    }
}