using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult Create(string Title, string Body, HttpPostedFileBase file, string Category)
        {
            Users user = new Users();
            Posts post = new Posts();
            Categories category = new Categories();
            BlogContext db = new BlogContext();
            int UserId = db.user.Single(usr => usr.Name.Equals(System.Web.HttpContext.Current.User.Identity.Name)).Id;
            if (ModelState.IsValid)
            {
                post.Id = db.post.Count() + 1;
                post.UserId = UserId;
                post.Title = Title;
                post.Body = Body;
                post.Date = DateTime.Now;
                category.id = db.post.Count() + 1;
                category.Name = Category;
                category.PostId = post.Id;
                category.UserId = UserId;
                if (file != null) file.SaveAs(HttpContext.Server.MapPath("~/Images/") + file.FileName);
                post.PhotoLocation = file.FileName;
                db.post.Add(post);
                db.category.Add(category);
                db.SaveChanges();
            }
            return View();
        }
    }
}
