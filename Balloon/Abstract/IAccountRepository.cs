using Balloon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Balloon.Abstract
{
    public interface IAccountRepository
    {
        IQueryable<ApplicationUser> Users { get; }

        void ChangeUserRoles(ApplicationUser user);
        ApplicationUser CreateNewUser();
        void Login(ApplicationUser user);
        void Logout(ApplicationUser user);
        void Register(ApplicationUser user);
    }
}