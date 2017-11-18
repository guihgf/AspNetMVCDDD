using AutoMapper;
using System;
using System.Web.Mvc;
using Upfinancas.MVC.Filters;
using Upfinancas.MVC.ViewModel;
using UpFinancas.App.Interfaces;
using UpFinancas.Domain.Entities;

namespace Upfinancas.MVC.Controllers
{
    [AutorizacaoFilter]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioApp _app;
        public UsuarioController(IUsuarioApp app)
        {
            _app = app;
        }

        // GET: Usuario
        [HttpGet]
        public ActionResult Index()
        {
            var usuario=(int)Session["USUARIO"];
            
            var usuarioViewModel=Mapper.Map<Usuario, UsuarioViewModel>(_app.Buscar(usuario));

            return View("Alterar", usuarioViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alterar(UsuarioViewModel usuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioId = (int)Session["USUARIO"];
                    var usuario = Mapper.Map<UsuarioViewModel, Usuario>(usuarioViewModel);
                    var usuarioAntigo= _app.Buscar(usuarioId);
                    usuario.Id = usuarioId;
                    usuario.DataConfirmacao = usuarioAntigo.DataConfirmacao;
                    _app.Salvar(usuario);
                    TempData["SUCESSO"] = "Dados alterados com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    TempData["ERRO"] = "Erro ao alterar os seus dados: " + e.Message;
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

    }
}