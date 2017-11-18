
using Microsoft.Practices.Unity;
using UpFinancas.App;
using UpFinancas.App.Interfaces;
using UpFinancas.Domain.Interfaces.Repositories;
using UpFinancas.Domain.Interfaces.Services;
using UpFinancas.Infra.Data.Repositories;
using UpFinancas.Infra.Data.Services;

namespace UpFinancas.Infra.CrossCutting
{
    public class LoadInjectionModule
    {
        public static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IUsuarioApp, UsuarioApp>();
            container.RegisterType<IUsuarioRepository, UsuarioRepository>();
            container.RegisterType<IContaApp, ContaApp>();
            container.RegisterType<IContaRepository, ContaRepository>();
            container.RegisterType<IEventoApp, EventoApp>();
            container.RegisterType<IEventoRepository, EventoRepository>();
            container.RegisterType<IMailService, MailService>();
            container.RegisterType<IPessoaApp, PessoaApp>();
            container.RegisterType<IPessoaRepository, PessoaRepository>();
            container.RegisterType<ICategoriaApp, CategoriaApp>();
            container.RegisterType<ICategoriaRepository, CategoriaRepository>();
            container.RegisterType<IFormaApp, FormaApp>();
            container.RegisterType<IFormaRepository, FormaRepository>();
        }
    }
}