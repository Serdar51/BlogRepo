using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class Register
    {
        public bool isValid(string _Name, string _EMailAddress)
        {
            BlogContext db = new BlogContext();

            int userForName = db.user.Count(x => x.EMailAddress.Equals(_Name));
            int userForEMail = db.user.Count(x => x.EMailAddress.Equals(_EMailAddress));

            // If there exists an account with the same user name it returns 1
            if (userForName != 0 || userForEMail != 0) return false;
            else return true;
        }
    }
}