using System.Net;
using System.Net.Http;
using System.Web.Http;
using UpFinancas.App.Interfaces;
using UpFinancas.Domain.Entities;

namespace Upfinancas.MVC.Controllers
{
    public class ContasController : ApiController
    {
        private readonly IContaApp _app;
        public ContasController(IContaApp app)
        {
            _app = app;
        }

        [HttpGet]
        [ActionName("Busca")]
        public HttpResponseMessage Buscar(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _app.Buscar(1, id)); 
        }

        [AcceptVerbs("POST","PUT")]
        public void Salvar(Conta conta)
        {
            
        }
    }
}
