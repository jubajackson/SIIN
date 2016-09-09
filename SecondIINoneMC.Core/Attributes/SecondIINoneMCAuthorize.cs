using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace SecondIINoneMC.Attributes
{
    public class SecondIINoneMCAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authroized = base.AuthorizeCore(httpContext);
            if (!authroized)
            {
                // the user is not authenticated or the forms authentication
                // cookie has expired
                return false;
            }

            // Now check the session:
            var myvar = httpContext.Session["UserId"];
            if (myvar == null)
            {
                // the session has expired
                return false;
            }

            return true;
        }
    }
}
