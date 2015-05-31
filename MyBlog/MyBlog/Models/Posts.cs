using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    [Table("Posts")]
    public class Posts
    {
        [Key]
        public int Id { set; get; }

        [Required]
        public string Title { set; get; }

        [Required]
        public string Body { set; get; }

        public string PhotoLocation { set; get; }

        public int UserId { set; get; }

        [Required]
        public DateTime Date { set; get; }

        [Required]
        public string Category { set; get; }
    }
}