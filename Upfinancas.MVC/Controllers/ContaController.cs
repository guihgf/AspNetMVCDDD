using System;
using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;
using Upfinancas.MVC.Filters;
using Upfinancas.MVC.ViewModel;
using UpFinancas.App.Interfaces;
using UpFinancas.Domain.Entities;
using Newtonsoft.Json;

namespace Upfinancas.MVC.Controllers
{
    [AutorizacaoFilter]
    public class ContaController : Controller
    {
        private readonly IContaApp _app;

        public ContaController(IContaApp app)
        {
            _app = app;
        }

        // GET: Conta
        public ActionResult Index()
        {
            var  usuario = (int)Session["USUARIO"];
            var contas = _app.Listar(usuario);

            var contaViewModel = Mapper.Map<IEnumerable<Conta>, IEnumerable<ContaViewModel>>(contas);
            return View(contaViewModel);
        }
        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(ContaViewModel contaView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var conta = Mapper.Map<ContaViewModel, Conta>(contaView);
                    conta.UsuarioId = (int)Session["USUARIO"];
                    _app.Salvar(conta);
                    TempData["SUCESSO"] = "Conta salva com sucesso!";
                    return RedirectToAction("Index");

                }
                catch (Exception e)
                {
                    TempData["ERRO"] = "Erro ao salvar a conta: " + e.Message;
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Editar(int contaId)
        {
            try
            {
                var conta = _app.Buscar((int) Session["USUARIO"], contaId);
                var contaViewModel = Mapper.Map<Conta, ContaViewModel>(conta);
                return View("Cadastrar",contaViewModel);

            }
            catch (Exception e)
            {
                TempData["ERRO"] = "Erro ao buscar a conta: " + e.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult AlterarStatus(int contaId)
        {
            try
            {
                _app.AlterarStatus(contaId,(int)Session["USUARIO"]);
                TempData["SUCESSO"] = "Conta atualizada com sucesso!";

                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                TempData["ERRO"] = "Erro ao atualizar a conta: " + e.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public String ListarAtivas()
        {
            try
            {
                var contaViewModel = Mapper.Map<IEnumerable<Conta>, IEnumerable<ContaViewModel>>(_app.ListarSomenteAtivas((int)Session["USUARIO"]));
                return JsonConvert.SerializeObject(contaViewModel, Formatting.Indented);
            }
            catch (Exception e)
            {

                return "Erro: " + e.Message;
            }

        }
    }
}