using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MyBlog.Controllers
{
    public class DisplayController : Controller
    {
        public ActionResult PostDetail(int id)
        {
            BlogContext db = new BlogContext();

            Posts post = new Posts();
             
            if (db.post.Any(o => o.Id == id))
            {
                ViewData["Error"] = null;
                post = db.post.Find(id);
            }
            else
            {
                ViewData["Error"] = "Cannot be found such a resource!!";
            }
            return View(post);
        }

        public ActionResult SearchCategory(string CategoryName, int? page)
        {
            BlogContext db = new BlogContext();

            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(db.post.Where(p => p.Category.Equals(CategoryName)).OrderBy(p => p.Date).ToPagedList(pageNumber, pageSize));
        }

    }
}
