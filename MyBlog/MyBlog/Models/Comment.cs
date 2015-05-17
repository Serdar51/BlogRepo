using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    [Table("Comments")]
    public class Comments
    {
        [Required]
        [Key]
        public int Id { set; get; }

        [Required]
        public int PostId { set; get; }

        [Required]
        public int UserId { set; get; }

        [Required]
        public DateTime Date { set; get; }

        [Required]
        public string Body { set; get; }
    }
}