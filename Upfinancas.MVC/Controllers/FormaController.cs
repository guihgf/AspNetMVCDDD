using System;
using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;
using Upfinancas.MVC.Filters;
using Upfinancas.MVC.ViewModel;
using UpFinancas.App.Interfaces;
using UpFinancas.Domain.Entities;

namespace Upfinancas.MVC.Controllers
{
    [AutorizacaoFilter]
    public class FormaController : Controller
    {
        private readonly IFormaApp _app;

        public FormaController(IFormaApp app)
        {
            _app = app;
        }

        public ActionResult Index()
        {
            var  usuario = (int)Session["USUARIO"];
            var formas = _app.Listar(usuario);

            var formaViewModel = Mapper.Map<IEnumerable<Forma>, IEnumerable<FormaViewModel>>(formas);
            return View(formaViewModel);
        }
        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(FormaViewModel formaView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var forma = Mapper.Map<FormaViewModel, Forma>(formaView);
                    forma.UsuarioId = (int)Session["USUARIO"];
                    _app.Salvar(forma);
                    TempData["SUCESSO"] = "Forma de pagamento/recebimento salva com sucesso!";
                    return RedirectToAction("Index");

                }
                catch (Exception e)
                {
                    TempData["ERRO"] = "Erro ao salvar a forma de pagamento/recebimento: " + e.Message;
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Editar(int formaId)
        {
            try
            {
                var forma = _app.Buscar((int) Session["USUARIO"], formaId);
                var formaViewModel = Mapper.Map<Forma, FormaViewModel>(forma);
                return View("Cadastrar", formaViewModel);

            }
            catch (Exception e)
            {
                TempData["ERRO"] = "Erro ao buscar a forma de pagamento/recebimento: " + e.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult AlterarStatus(int formaId)
        {
            try
            {
                _app.AlterarStatus(formaId,(int)Session["USUARIO"]);
                TempData["SUCESSO"] = "Forma de pagamento/recebimento atualizada com sucesso!";

                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                TempData["ERRO"] = "Erro ao atualizar a forma de pagamento/recebimento: " + e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}