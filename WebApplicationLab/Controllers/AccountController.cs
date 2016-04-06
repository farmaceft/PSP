using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WebApplicationLab.Models;

namespace WebApplicationLab.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (ValidateLoginUser(model.UserName))
                {
                    CreateUser(model.UserName, model.Password);
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с данным логином уже существует");
                }
            }
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        private void CreateUser(string login, string password)
        {
            using (HelpDeskContext db = new HelpDeskContext())
            {
                try
                {
                    db.Users.Add(new User() {Login = login, Password = password, RoleId = 2});
                    db.SaveChanges();
                }
                catch
                {
                    // ignored
                }
            }
        }
        private bool ValidateLoginUser(string login)
        {
            bool isValid = true;

            using (HelpDeskContext db = new HelpDeskContext())
            {
                try
                {
                    User user = (from u in db.Users
                                 where u.Login == login
                                 select u).FirstOrDefault();

                    if (user != null)
                    {
                        isValid = false;
                    }
                }
                catch
                {
                    isValid = true;
                }
            }
            return isValid;
        }

        private bool ValidateUser(string login, string password)
        {
            bool isValid = false;

            using (HelpDeskContext db = new HelpDeskContext())
            {
                try
                {
                    User user = (from u in db.Users
                                 where u.Login == login && u.Password == password
                                 select u).FirstOrDefault();

                    if (user != null)
                    {
                        isValid = true;
                    }
                }
                catch
                {
                    isValid = false;
                }
            }
            return isValid;
        }
    }
}
