using Microsoft.AspNet.Identity.EntityFramework;

namespace Balloon.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() { }

        public string Description { get; set; }
    }
}