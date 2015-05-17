using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int Id { set; get; }

        [Required]
        public string Password { set; get; }

        [Required]
        public string Name { set; get; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EMailAddress { set; get; }
    }
}