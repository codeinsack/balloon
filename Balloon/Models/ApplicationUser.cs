using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Balloon.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string NickName { get; set; }
        public List<Article> Articles { get; set; }
        public List<Order> Orders { get; set; }

        public ApplicationUser()
        {
            Articles = new List<Article>();
        }
    }
}