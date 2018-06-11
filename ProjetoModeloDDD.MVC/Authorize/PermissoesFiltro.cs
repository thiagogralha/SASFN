using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SASF.MVC.Authorize
{
    public class PermissoesFiltro : System.Web.Mvc.AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.Result is HttpUnauthorizedResult)
            {   
                filterContext.HttpContext.Response.Redirect("~/Login");
            }
        }
    }
}