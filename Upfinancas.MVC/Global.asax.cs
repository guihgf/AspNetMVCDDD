using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity.Mvc;
using Upfinancas.MVC.Mappers;
using UpFinancas.Infra.CrossCutting;
using Upfinancas.MVC.ViewModel;
using System.Web.Http;

namespace Upfinancas.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //IOC
            var container = LoadInjectionModule.BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // Configurando o AutoMapper para registrar os profiles
            // de mapeamento durante a inicialização da aplicação.
            AutoMapperConfig.RegisterMappings();


            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());

            
        }

    }
}
