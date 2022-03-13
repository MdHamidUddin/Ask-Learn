using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AskNLearn.Controllers
{
    public class LearnerController : Controller
    {
        // GET: Learner
        public ActionResult Index()
        {
            return View();
        }
    }
}