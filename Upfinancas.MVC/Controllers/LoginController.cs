using System;
using System.Web.Mvc;
using AutoMapper;
using Upfinancas.MVC.ViewModel;
using UpFinancas.App.Interfaces;
using UpFinancas.Domain.Entities;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;

namespace Upfinancas.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioApp _app;

        public LoginController(IUsuarioApp app)
        {
            _app = app;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Autenticar(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario=_app.Autenticar(login.Email, login.Senha);
                    Session["USUARIO"] = usuario.Id;
                    Session["USUARIO_NOME"] = usuario.Nome;

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    TempData["ERRO"] = e.Message;
                }
                
            }
                
            return View("Index");
        }

        [HttpGet]
        public ActionResult Sair()
        {
            Session.Abandon();            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(UsuarioViewModel usuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                //validate captcha
                if (Session["Captcha"] == null || Session["Captcha"].ToString() != usuarioViewModel.Captcha)
                {
                    ModelState.AddModelError("Captcha", "Valor incorreto da soma, por favor, tente novamente.");
                    //dispay error and generate a new captcha
                    return View(usuarioViewModel);
                }

                try
                {
                    var usuario = Mapper.Map<UsuarioViewModel, Usuario>(usuarioViewModel);
                    _app.Salvar(usuario);
                    TempData["SUCESSO"] = "O usuário e a senha para acesso foram enviados em seu e-mail.";
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    TempData["ERRO"] = e.Message;
                    return View(usuarioViewModel);
                }
            }
            return View(usuarioViewModel);
        }

        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            //generate new question
            int a = rand.Next(10, 99);
            int b = rand.Next(0, 9);
            var captcha = string.Format("{0} + {1} = ?", a, b);

            //store answer
            Session["Captcha" + prefix] = a + b;

            //image stream
            FileContentResult img = null;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));

                        r = rand.Next(0, (130 / 3));
                        x = rand.Next(0, 130);
                        y = rand.Next(0, 30);

                        gfx.DrawEllipse(pen, (x - r), (y - r), r, r);
                    }
                }

                //add question
                gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);

                //render as Jpeg
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }

            return img;
        }

        protected override void Dispose(bool disposing)
        {
            _app.Dispose();
        }
    }
}