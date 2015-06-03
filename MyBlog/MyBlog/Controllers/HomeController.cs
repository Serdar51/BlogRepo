using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;
using PagedList;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        private BlogContext db = new BlogContext();

        public ViewResult Index(int? page)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(db.post.OrderBy(p => p.Date).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Search(string q, int? page)
        {
            ViewData["Key"] = q;
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(db.post.SqlQuery("Select * from Posts where Body like '%" + q + "%' or Title like '%" + q + "%'").OrderBy(p => p.Date).ToPagedList(pageNumber, pageSize));
        }
    }
}