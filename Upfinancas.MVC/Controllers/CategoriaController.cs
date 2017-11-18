using System;
using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;
using Upfinancas.MVC.Filters;
using Upfinancas.MVC.ViewModel;
using UpFinancas.App.Interfaces;
using UpFinancas.Domain.Entities;
using System.Web.Script.Serialization;

namespace Upfinancas.MVC.Controllers
{
    [AutorizacaoFilter]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaApp _app;

        public CategoriaController(ICategoriaApp app)
        {
            _app = app;
        }

        public ActionResult Index()
        {
            var  usuario = (int)Session["USUARIO"];
            var categorias = _app.Listar(usuario);

            var categoriaViewModel = Mapper.Map<IEnumerable<Categoria>, IEnumerable<CategoriaViewModel>>(categorias);
            return View(categoriaViewModel);
        }
        [HttpGet]
        public ActionResult Cadastrar()
        {
            var categoriaViewModel = new CategoriaViewModel();
            return View(categoriaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(CategoriaViewModel categoriaView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                
                    var categoria = Mapper.Map<CategoriaViewModel, Categoria>(categoriaView);
                    categoria.UsuarioId = (int)Session["USUARIO"];
                    _app.Salvar(categoria);
                    TempData["SUCESSO"] = "Categoria salva com sucesso!";
                    return RedirectToAction("Index");

                }
                else
                {
                    throw new Exception("Campos inválidos!");
                }
                
            }
            catch (Exception e)
            {
                TempData["ERRO"] = "Erro ao salvar a categoria: " + e.Message;
                categoriaView.MontarListaTipos(categoriaView.Tipo);
                return View(categoriaView);
            }
        }
        [HttpGet]
        public ActionResult Editar(int categoriaId)
        {
            try
            {
                var categoria = _app.Buscar((int) Session["USUARIO"], categoriaId);
                var categoriaViewModel = Mapper.Map<Categoria, CategoriaViewModel>(categoria);
                return View("Cadastrar", categoriaViewModel);

            }
            catch (Exception e)
            {
                TempData["ERRO"] = "Erro ao buscar a categoria: " + e.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult AlterarStatus(int categoriaId)
        {
            try
            {
                _app.AlterarStatus(categoriaId, (int)Session["USUARIO"]);
                TempData["SUCESSO"] = "Categoria atualizada com sucesso!";

                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                TempData["ERRO"] = "Erro ao atualizar a categoria: " + e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public string ListaPorTipo(int tipo)
        {
            try
            {
                   var categoriaViewModel = Mapper.Map<IEnumerable<Categoria>, IEnumerable<CategoriaViewModel>>(_app.ListarSomenteAtivasPorTipo((int)Session["USUARIO"], tipo));
                    return  new JavaScriptSerializer().Serialize(categoriaViewModel);
            }
            catch (Exception e)
            {
                return "Erro ao atualizar a categoria: " + e.Message;
            }
        }
    }
}