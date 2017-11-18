using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Upfinancas.MVC.Filters;

namespace Upfinancas.MVC.Controllers
{
    [AutorizacaoFilter]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}