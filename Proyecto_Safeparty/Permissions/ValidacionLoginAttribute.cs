using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web;
using Proyecto_Safeparty.Controllers;
using Microsoft.AspNetCore.Http;
using Proyecto_Safeparty.Models;


namespace Proyecto_Safeparty.Permissions
{
    public class ValidacionLoginAttribute : ActionFilterAttribute
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ValidacionLoginAttribute(IHttpContextAccessor HttpContextAccessor)
        {
            _contextAccessor = HttpContextAccessor;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var isEnCarpetaHome = filterContext.ActionDescriptor.RouteValues.ContainsKey("controller") && filterContext.ActionDescriptor.RouteValues["controller"] == "Home";

            if (isEnCarpetaHome)
            {
                if (_contextAccessor.HttpContext.Session.GetString("Username") == null)
                {
                    filterContext.Result = new RedirectResult("~/Acceso/Login");
                }
            }
            

            
            base.OnActionExecuting(filterContext);
        }
    }
}
