using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        private BlogContext db = new BlogContext();

        public ActionResult Index()
        {
            return View(db.post.ToList());
        }

        [HttpPost]
        public ActionResult Search(string q)
        {
            return View(db.post.SqlQuery("Select * from Posts where Body like '%" + q + "%' or Title like '%" + q + "%'").ToList());
        }
    }
}