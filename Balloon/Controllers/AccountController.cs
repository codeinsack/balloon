using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Balloon.Models;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net;

namespace Balloon.Controllers
{
    public class AccountController : Controller
    {
        ApplicationContext db = new ApplicationContext();

        [Authorize(Roles = "Администратор")]
        public ActionResult Index()
        {
            var roles = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            List<UserViewModel> users = new List<UserViewModel>();

            users.AddRange(from u in db.Users
                           select new UserViewModel
                           {
                               Id = u.Id,
                               Email = u.Email,
                               NickName = u.NickName,
                               Role = roles.Roles.FirstOrDefault(user => user.Users.Any(r => r.UserId == u.Id))
                           });

            return View(users);
        }

        [Authorize(Roles = "Администратор")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }


                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        NickName = model.NickName,
                        EmailConfirmed = true
                    };

                    var result = await UserManager.CreateAsync(user, model.Password);

                    

                    string temp = Request.Form["Roles"].ToString();

                    var userStore = new UserStore<ApplicationUser>(db);
                    var userManager = new UserManager<ApplicationUser>(userStore);
                    userManager.AddToRole(user.Id, temp);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Account");
                    }
                }
            }
            catch { }

            ViewBag.Role = new SelectList(db.Roles, "Id", "Name");

            return View(model);
        }

        // Регистрация
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, NickName = model.NickName };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        // Авторизация
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль!");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;

            return View(model);
        }
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }

        // Редактирование и удаление пользователей
        [Authorize(Roles = "Администратор")]
        public async Task<ActionResult> Delete(string userId)
        {
            if (userId == null || userId.Equals(string.Empty))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            var model = new UserViewModel
            {
                Email = user.Email,
                NickName = user.NickName,
                Id = user.Id,
                Password = "Фейковый пароль"
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserViewModel model)
        {
            ApplicationContext db = new ApplicationContext();
            IQueryable<Article> articles = from o in db.Articles
                                           where o.UserId == model.Id
                                           select o;
            ApplicationUser user = db.Users.Find(model.Id);
            foreach (Article article in articles)
            {
                db.Articles.Remove(article);
            }
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Администратор")]
        public async Task<ActionResult> Edit(string userId)
        {
            if (userId == null || userId.Equals(string.Empty))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            var model = new UserViewModel
            {
                Email = user.Email,
                NickName = user.NickName,
                Id = user.Id,
                Password = user.PasswordHash
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (ModelState.IsValid)
                {
                    var user = await UserManager.FindByIdAsync(model.Id);
                    if (user != null)
                    {
                        user.Email = model.Email;
                        user.UserName = model.Email;
                        user.NickName = model.NickName;
                        if (!user.PasswordHash.Equals(model.Password))
                            user.PasswordHash = UserManager.PasswordHasher.HashPassword(model.Password);

                        var result = await UserManager.UpdateAsync(user);

                        var userStore = new UserStore<ApplicationUser>(db);
                        var userManager = new UserManager<ApplicationUser>(userStore);

                        string temp = Request.Form["Roles"].ToString();

                        userManager.AddToRole(user.Id, temp);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Account");
                        }
                    }
                }
            }
            catch { }
            return View(model);
        }
    }
}