//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace MYPAY.Models
//{
//    public class SessionExpireAttribute : ActionFilterAttribute
//    {
//        public override void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            HttpContext ctx = HttpContext.Current;
//            // check  sessions here
//            if (HttpContext.Current.Session["custid"] == null)
//            {
//                filterContext.Result = new RedirectResult("~/Login/LoginView");
//                return;
//            }
//            base.OnActionExecuting(filterContext);
//        }
//    }
//}