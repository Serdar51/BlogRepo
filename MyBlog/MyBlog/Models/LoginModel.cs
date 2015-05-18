using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace MyBlog.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }


        public bool IsValid(string _username, string _password)
        {
            BlogContext db = new BlogContext();

            if (db.user.Count(x => x.Name.Equals(_username)) != 0)
            {
                Users user = db.user.Single(x => x.Name.Equals(_username));

                if (user.Password.Equals(_password))
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
    }
}