using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.ViewModels;

namespace WebApplication2.Helpers
{
    public class PermisionRequired  :ActionFilterAttribute
    {
        Login.Permissions expectedPermissions;
        //LoginForm.Permissions ex;

        public PermisionRequired(Login.Permissions permissions = Login.Permissions.None)
        {
            this.expectedPermissions = permissions;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ActionResult unauthorizedActionResult = new ViewResult { ViewName = "Unauthorized" };
            ActionResult logoutActionResult = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Authenticate", action = "Logout", returnUrl = filterContext.HttpContext.Request.RawUrl }));

            var user = filterContext.HttpContext.Session["User"] as Login;

            if (user == null)
            {
                filterContext.Result = logoutActionResult;
                return;
            }

            //var userCurrentPermissions = user.CurrentPermissions;
            var userCurrentPermissions = (Login.Permissions)HttpContext.Current.Cache[string.Format("{0}'s CurrentPermision", user.Username)];

            if (((userCurrentPermissions & expectedPermissions) != expectedPermissions))
                filterContext.Result = unauthorizedActionResult;
        }
    }
}