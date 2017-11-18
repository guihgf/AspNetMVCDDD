using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace Upfinancas.MVC.Filters
{
    public class AutorizacaoFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            Controller controller = filterContext.Controller as Controller;

            //comentar em prod
            //session["USUARIO"] = 1;
            //session["USUARIO_NOME"] = "Guilherme Fermino";

            if (controller != null)
            {
                if (session != null && session["USUARIO"] == null)
                {
                    filterContext.Result =
                        new RedirectToRouteResult(
                            new RouteValueDictionary
                            {
                                {"controller", "Login"},
                                {"action", "Index"}

                            });
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}