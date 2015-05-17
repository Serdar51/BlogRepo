using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class BlogContext : DbContext
    {
        public DbSet<Users> user { set; get; }
        public DbSet<Posts> post { set; get; }
        public DbSet<Comments> comment { set; get; }
        public DbSet<Categories> category { set; get; }
    }
}