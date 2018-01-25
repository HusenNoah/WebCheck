using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.ViewModels;
using WebApplication2.Models;
using System.Web.Security;

namespace WebApplication2.Controllers
{
    public class AuthenticateController : Controller
    {
        Repository _repository = new Repository();
        // GET: Authenticate
        public ActionResult Login()
        {
            return View(_repository);
        }
        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var User = _repository.Authenticate(login.Username, login.Password);
                if (User != null)
                {
                    Session["User"] = User;
                    Session["username"] = User.Username;
                    HttpContext.Cache[string.Format("{0}'s CurrentPermision", login.Username)] = User.CurrentPermissions;
                    FormsAuthentication.SetAuthCookie(User.Username, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            
            FormsAuthentication.SignOut();
            Session["User"] = null;
            Session["username"] = null;
            return RedirectToAction("Login");
        }

        public string Content()
        {
            return "Hello";
        }

    }
}