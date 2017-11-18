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
    public class EventoController : Controller
    {
        private readonly IEventoApp _app;
        private readonly IContaApp _contaApp;
        private readonly IFormaApp _formaApp;
        private readonly IPessoaApp _pessoaApp;
        private readonly ICategoriaApp _categoriaApp;

        public EventoController(IEventoApp app,IContaApp contaApp, IFormaApp formaApp, IPessoaApp pessoaApp,ICategoriaApp categoriaApp)
        {
            _app = app;
            _contaApp = contaApp;
            _formaApp = formaApp;
            _pessoaApp = pessoaApp;
            _categoriaApp = categoriaApp;

        }

        // GET: Conta
        public ActionResult Index()
        {
            var  usuario = (int)Session["USUARIO"];
            var eventos = _app.Listar(usuario);

            var eventoViewModel = Mapper.Map<IEnumerable<Evento>, IEnumerable<EventoViewModel>>(eventos);
            return View(eventoViewModel);
        }
        [HttpGet]
        public ActionResult Cadastrar()
        {
            try
            {
                
                var usuario = (int)Session["USUARIO"];
                var contas = _contaApp.ListarSomenteAtivas(usuario);
                var formas = _formaApp.ListarSomenteAtivas(usuario);
                var pessoas = _pessoaApp.ListarSomenteAtivas(usuario);
                var categorias = _categoriaApp.ListarSomenteAtivasPorTipo(usuario,(int)ETipoCategoria.Despesa);
                var eventoView = new EventoViewModel(contas, formas,pessoas, categorias);

                return View(eventoView);

            }
            catch(Exception e)
            {
                TempData["ERRO"] = "Erro ao buscar dados para o lançamento: " + e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(EventoViewModel eventoView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var evento = Mapper.Map<EventoViewModel, Evento>(eventoView);
                    _app.Salvar(evento);
                    TempData["SUCESSO"] = "Evento salvo com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception("Campos inválidos.");
                }
            }
            catch (Exception e)
            {
                TempData["ERRO"] = "Erro ao salvar o evento: " + e.Message;
                var usuario = (int)Session["USUARIO"];
                var contas = _contaApp.ListarSomenteAtivas(usuario);
                var formas = _formaApp.ListarSomenteAtivas(usuario);
                var pessoas = _pessoaApp.ListarSomenteAtivas(usuario);
                var categorias = _categoriaApp.ListarSomenteAtivasPorTipo(usuario, eventoView.Tipo);
                eventoView.MontarListaContas(contas, eventoView.ContaId);
                eventoView.MontarListaFormas(formas, eventoView.FormaId);
                eventoView.MontarListaPessoas(pessoas, (int)(eventoView.PessoaId!=null? eventoView.PessoaId:0));
                eventoView.MontarListaCategorias(categorias, (int)(eventoView.CategoriaId != null ? eventoView.CategoriaId : 0));
                eventoView.MontarListaTipos(eventoView.Tipo);
                return View(eventoView);
            }
        }
        [HttpGet]
        public ActionResult Editar(int eventoId)
        {
            try
            {
                var usuario = (int)Session["USUARIO"];
                var evento = _app.Buscar(usuario, eventoId);
                var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);
                eventoViewModel.MontarListaContas(_contaApp.ListarSomenteAtivas(usuario),eventoViewModel.ContaId);
                eventoViewModel.MontarListaFormas(_formaApp.ListarSomenteAtivas(usuario), eventoViewModel.FormaId);
                eventoViewModel.MontarListaPessoas(_pessoaApp.ListarSomenteAtivas(usuario), (int)(eventoViewModel.PessoaId != null ? eventoViewModel.PessoaId : 0));
                eventoViewModel.MontarListaCategorias(_categoriaApp.ListarSomenteAtivasPorTipo(usuario, eventoViewModel.Tipo), (int)(eventoViewModel.CategoriaId != null ? eventoViewModel.CategoriaId : 0));
                return View("Cadastrar", eventoViewModel);

            }
            catch (Exception e)
            {
                TempData["ERRO"] = "Erro ao buscar o evento: " + e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Excluir(int eventoId)
        {
            try
            {
                var usuario = (int)Session["USUARIO"];
                _app.Excluir(eventoId, usuario);
                TempData["SUCESSO"] = "Evento removido com sucesso.";
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                TempData["ERRO"] = "Erro ao remover o evento: " + e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public String ListarEventosPorConta(int contaId)
        {
            try
            {
                var eventos = _app.ListarPorConta((int)Session["USUARIO"],contaId);

                var contaViewModel = Mapper.Map<IEnumerable<Evento>, IEnumerable<EventoViewModel>>(eventos);
                return JsonConvert.SerializeObject(contaViewModel, Formatting.Indented);
            }
            catch (Exception e)
            {

                return "Erro: " + e.Message;
            }

        }

        [HttpGet]
        public ActionResult Transferencia()
        {
            return View("Transferencia", new TransferenciaViewModel(_contaApp.ListarSomenteAtivas((int)Session["USUARIO"])));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transferencia(TransferenciaViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Console.Write("teste");
                    return View(viewModel);
                }
                else
                {
                    throw new Exception("Campos inválidos.");
                }
            }
            catch(Exception e)
            {
                viewModel.MontarListaContas(_contaApp.ListarSomenteAtivas((int)Session["USUARIO"]), viewModel.ContaSaidaId);
                return View(viewModel);
            }
           
        }

    }
}