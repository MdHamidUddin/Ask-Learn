using AskNLearn.Models;
using AskNLearn.Models.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Security;

namespace AskNLearn.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        AskNLearnEntities dbObj = new AskNLearnEntities();
        string uname;
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", (string)Session["userType"]);
            }
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

                if (data != null && data.username.FirstOrDefault().Equals('A') && data.approval.Equals("active"))
                {
                    FormsAuthentication.SetAuthCookie(data.username, false);
                    Session["username"] = data.username;
                    Session["name"] = data.name;
                    Session["userType"] = "Admin";
                    Session["adminProfilePic"] = data.proPic;
                    return RedirectToAction("../Admin/Dashboard");
                }
                else if (Data != null && data.username.FirstOrDefault().Equals('I') && data.approval.Equals("active"))
                {
                    FormsAuthentication.SetAuthCookie(data.username, false);
                    Session["name"] = data.name;
                    Session["uid"] = data.uid;
                    Session["proPic"] = data.proPic;
                    Session["userType"] = "Instructor";
                    return RedirectToAction("../Instructor/Dashboard");
                }
                else if (Data != null && data.username.FirstOrDefault().Equals('M') && data.approval.Equals("active"))
                {
                    FormsAuthentication.SetAuthCookie(data.username, false);
                    Session["username"] = data.username;
                    Session["name"] = data.name;
                    Session["userType"] = "Moderator";
                    Session["adminProfilePic"] = data.proPic;
                    return RedirectToAction("../Moderator/Dashboard");
                }
                else if (Data != null && data.username.FirstOrDefault().Equals('L') && data.approval.Equals("active"))
                {
                    FormsAuthentication.SetAuthCookie(data.username, false);
                    Session["username"] = data.username;
                    Session["uid"] = data.uid;
                    Session["userType"] = "Learner";
                    return RedirectToAction("../gg/Dashboard");
                }
                else if (Data == null)
                {
                    ViewBag.Message = "Your Username Or Password May Be Incorrect";
                }
                else if (data.approval.Equals("pending"))
                {
                    ViewBag.Message = "Your Account Is Still Pending. Contact Admin For Approval";
                }
                else if (data.approval.Equals("blocked"))
                {
                    ViewBag.Message = "Your Are Blocked!. Contact Admin";
                }
            }

            return View();
        }
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(SignUpModel Model)
        {
            User user = new User();
            UsersInfo userInfo = new UsersInfo();
            DateTime time = DateTime.Now;
            if(ModelState.IsValid)
            {
                string serial=UserSerial(Model.userType);
                user.name = Model.name;
                user.username = serial +"-"+ Model.username;
                user.email = Model.email;
                user.password = Model.password;
                user.dob = Model.dob;
                user.gender = Model.gender;
                user.userType = Model.userType;
                user.approval = "pending";
                user.dateTime = time;
                dbObj.Users.Add(user);
                dbObj.SaveChanges();

                userInfo.uid = user.uid;
                userInfo.eduInfo = Model.eduInfo;
                userInfo.currentPosition = Model.currentPosition;
                userInfo.reputation = 0;
                dbObj.UsersInfoes.Add(userInfo);
                dbObj.SaveChanges();

           
                uname = "Username : "+user.username;
                BuildEmailTemplate(user.uid);
                return RedirectToAction("Login");
            }
            return View();
        }

        string UserSerial(string uType)
        {
            var UserSerial = 0;
            var uname = "";
            var user = dbObj.Users.Where(x=>x.userType.Equals(uType)).ToList();
            var count = dbObj.Users.Where(x => x.userType.Equals(uType)).Count();


            if (count==0)
            {
                string type = uType.Substring(0, 1);
                uname = type + "1";
            }
            else if  (count>=1)
            {
                foreach (var u in user)
                {
                    uname = u.username;
                }

                string type = uname.Substring(0, 1);
                uname = uname.Substring(1, 1);
                UserSerial = Convert.ToInt32(uname);
                UserSerial++;
                uname = Convert.ToString(UserSerial);
                uname = type + uname;
            }
           
          

            return uname;
        }


        public void BuildEmailTemplate(int uid)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "Text" + ".cshtml");
            var regInfo = dbObj.Users.Where(x => x.uid == uid).FirstOrDefault();
            var url = "https://localhost:44343/" + "Register/Confirm?regId=" + uid;
            body = body.Replace("@ViewBag.ConfirmationLink", url);
            body = body.ToString();
            BuildEmailTemplate("Your Account is Successfully Created", body, regInfo.email,uname);
        }

        public static void BuildEmailTemplate(string subjectText, string bodyText, string sendTo,string uname)
        {
            string from, to, bcc, cc, subject, body;
            from = "hamiduddin09@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            sb.Append(uname);
            body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));

            }

            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));

            }

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);
        }

        public static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("hamiduddin09@gmail.com", "Neverstoplearning1998");
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;

            }


        }
















        public ActionResult Logout()
        {
            Session.Clear();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            //FormsAuthentication.RedirectToLoginPage();
            return RedirectToAction("Login", "Users");
        }

    }
}