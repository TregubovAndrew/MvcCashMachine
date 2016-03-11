using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CashMachine.Web.Helpers;

namespace CashMachine.Web.Filters
{
    public class CheckFullAuthentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
            {
                var session = (AccountSession) filterContext.HttpContext.Session["account"];
                if (session != null && session.CardNumber != null && !session.IsAuthenticated)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            {"Controller", "Account"},
                            {"Action", "PinCode"}
                        });
                }
                base.OnActionExecuting(filterContext);
            }
        }
    }
}