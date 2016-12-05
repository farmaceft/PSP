using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using SHA3;
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

        private string GenerateHash(string password, string salt)
        {
            SHA3Unmanaged sha3 = new SHA3Unmanaged(512);
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(password + salt + WebConfigurationManager.AppSettings["globalSalt"]);
            byte[] hash = sha3.ComputeHash(bytes);
            return ByteArrayToString(hash);
        }

        private string GenerateSalt()
        {
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            byte[] saltInBytes = new byte[12];
            crypto.GetBytes(saltInBytes);
            return ByteArrayToString(saltInBytes);
        }

        private static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (!ValidateUser(model.UserName, null))
                {
                    string salt = GenerateSalt();
                    if (CreateUser(model.UserName, GenerateHash(model.Password, salt), salt))
                    {
                        return SetAuthCookie(model.UserName, model.RememberMe, returnUrl);
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
                    return SetAuthCookie(model.UserName, model.RememberMe, returnUrl);
                }
                ModelState.AddModelError("", "Неправильный пароль или логин");
            }
            return View(model);
        }

        private ActionResult SetAuthCookie(string userName, bool rememberMe, string returnUrl)
        {
            FormsAuthentication.SetAuthCookie(userName, rememberMe);
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        private bool CreateUser(string login, string password, string salt)
        {
            using (HelpDeskContext db = new HelpDeskContext())
            {
                try
                {
                    db.Users.Add(new User { Login = login, Password = password, RoleId = 2, Salt = salt });
                    db.SaveChanges();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
        
        private bool ValidateUser(string login, string password)
        {
            bool isValid = false;

            using (HelpDeskContext db = new HelpDeskContext())
            {
                try
                {
                    User user = db.Users.FirstOrDefault(u => u.Login == login);

                    if (user != null && password == null || user != null && Equals(user.Password, GenerateHash(password, user.Salt)))
                        isValid = true;
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