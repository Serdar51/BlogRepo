using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;
using System.Web.Security;

namespace MyBlog.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsValid(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(string Name, string EMailAddress, string Password)
        {
            Register register = new Register();
            BlogContext db = new BlogContext();

            if (ModelState.IsValid)
            {
                if (register.isValid(Name, EMailAddress))
                {
                    Users user = new Users();
                    int num = db.user.Count();
                    user.Id = num + 1;
                    user.Password = Password;
                    user.EMailAddress = EMailAddress;
                    user.Name = Name;
                    db.user.Add(user);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(Name, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "There already exist an account that has the followings. Please enter different user name or email address!!");
                }
            }
            return View();
        }
    }
}
