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
    public class PessoaController : Controller
    {
        private readonly IPessoaApp _app;

        public PessoaController(IPessoaApp app)
        {
            _app = app;
        }

        public ActionResult Index()
        {
            var  usuario = (int)Session["USUARIO"];
            var pessoas = _app.Listar(usuario);

            var pessoaViewModel = Mapper.Map<IEnumerable<Pessoa>, IEnumerable<PessoaViewModel>>(pessoas);
            return View(pessoaViewModel);
        }
        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(PessoaViewModel pessoaView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pessoa = Mapper.Map<PessoaViewModel, Pessoa>(pessoaView);
                    pessoa.UsuarioId = (int)Session["USUARIO"];
                    _app.Salvar(pessoa);
                    TempData["SUCESSO"] = "Pessoa salva com sucesso!";
                    return RedirectToAction("Index");

                }
                catch (Exception e)
                {
                    TempData["ERRO"] = "Erro ao salvar a pessoa: " + e.Message;
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Editar(int pessoaId)
        {
            try
            {
                var pessoa = _app.Buscar((int) Session["USUARIO"], pessoaId);
                var pessoaViewModel = Mapper.Map<Pessoa, PessoaViewModel>(pessoa);
                return View("Cadastrar", pessoaViewModel);

            }
            catch (Exception e)
            {
                TempData["ERRO"] = "Erro ao buscar a pessoa: " + e.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult AlterarStatus(int pessoaId)
        {
            try
            {
                _app.AlterarStatus(pessoaId,(int)Session["USUARIO"]);
                TempData["SUCESSO"] = "Pessoa atualizada com sucesso!";

                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                TempData["ERRO"] = "Erro ao atualizar a pessoa: " + e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}