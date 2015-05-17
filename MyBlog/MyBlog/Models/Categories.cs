using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    [Table("Categories")]
    public class Categories
    {
        [Key]
        public int id { set; get; }

        public string Name { set; get; }

        public int PostId { get; set; }

        public int UserId { set; get; }
    }
}