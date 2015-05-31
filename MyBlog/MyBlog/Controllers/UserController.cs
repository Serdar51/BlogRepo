using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class UserController : Controller
    {

        public ActionResult Index()
        {
            BlogContext db = new BlogContext();
            List<Posts> list = new List<Posts>();
            if (db.user.Any(info => info.Name == System.Web.HttpContext.Current.User.Identity.Name))
            {
                list = db.post.SqlQuery("Select * from Posts where userid='" + db.user.Single(usr => usr.Name.Equals(System.Web.HttpContext.Current.User.Identity.Name)).Id + "'").ToList();
            }
            return View(list);
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
            BlogContext db = new BlogContext();
            int UserId = db.user.Single(usr => usr.Name.Equals(System.Web.HttpContext.Current.User.Identity.Name)).Id;
            if (ModelState.IsValid)
            {
                post.Id = db.post.Count() + 1;
                post.UserId = UserId;
                post.Title = Title;
                post.Body = Body;
                post.Date = DateTime.Now;
                post.Category = Category;
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/") + post.Id + file.FileName);
                    post.PhotoLocation = post.Id + file.FileName;
                }
                
                db.post.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            else
            {
                ModelState.AddModelError("","Check your inputs!");
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Request.IsAuthenticated)
            {
                BlogContext db = new BlogContext();
                if (db.post.Any(p => p.Id == id))
                {
                    Posts post = new Posts();
                    post = db.post.Find(id);
                    return View(post);
                }
                else
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult Edit(Posts post, HttpPostedFileBase file)
        {
            BlogContext db = new BlogContext();
            Users user = new Users();
            Posts postTemp = new Posts();
            string PhotoLocation;

            if (ModelState.IsValid)
            {
                postTemp = db.post.Find(post.Id);
                PhotoLocation = postTemp.PhotoLocation;
                postTemp.Title = post.Title;
                postTemp.Body = post.Body;
                postTemp.Category = post.Category;
                if (file != null)
                {
                    postTemp.PhotoLocation = post.Id + file.FileName;
                }
                db.SaveChanges();
                
                if (file != null)
                {
                    string fullPath = Request.MapPath("~/Images/" + PhotoLocation);

                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/") + post.Id + file.FileName);
                }
                return RedirectToAction("index", "user");
            }
            else
            {
                return View(post);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            BlogContext db = new BlogContext();
            Posts post = new Posts();
            if (Request.IsAuthenticated)
            {
                post = db.post.Find(id);
                return View(post);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult Delete(int Id, int UserId, string PhotoLocation)
        {
            Posts post = new Posts();
            BlogContext db = new BlogContext();

            if (ModelState.IsValid)
            {
                post = db.post.Single(p => p.Id == Id);
                string fullPath = Request.MapPath("~/Images/" + PhotoLocation);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                db.post.Remove(post);
                db.SaveChanges();
            }
            return RedirectToAction("Index","User");
        }
    }
}
