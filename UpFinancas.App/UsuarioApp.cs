using System;
using UpFinancas.App.Interfaces;
using UpFinancas.Domain.Entities;
using UpFinancas.Domain.Interfaces.Repositories;
using UpFinancas.Domain.Interfaces.Services;
using UpFinancas.Util;

namespace UpFinancas.App
{
    public class UsuarioApp:IUsuarioApp
    {
        private readonly IUsuarioRepository _repo;
        private readonly IMailService _mail;
        private readonly ICategoriaApp _categoriaApp;
        private readonly IContaApp _contaApp;
        private readonly IFormaApp _formaApp;

        public UsuarioApp(IUsuarioRepository repo,IMailService mail,ICategoriaApp categoriaApp,IContaApp contaApp,IFormaApp formaApp)
        {
            _repo = repo;
            _mail = mail;
            _categoriaApp = categoriaApp;
            _contaApp = contaApp;
            _formaApp = formaApp;
        }
        public void Salvar(Usuario usuario)
        {
            if (usuario.Id > 0)
            {
                var usuarioDadosAnt = this.Buscar(usuario.Id);
                //se houve alteração de senha,encripta nova senha
                if ((usuario.Senha != null) && (usuario.Senha != usuarioDadosAnt.Senha))
                    usuario.EncriptarSenha();
                else
                    usuario.Senha = usuarioDadosAnt.Senha;

                _repo.Alterar(usuario);
            }
            else
            {
                var senha = usuario.GerarSenha();

                var usuarioCadastrado=_repo.BuscaPorEmail(usuario.Email);

                if (usuarioCadastrado != null)
                    throw new Exception("E-mail já cadastrado!");

                _repo.Salvar(usuario);

                var mailTemplate = new MailTemplate(usuario.Email, usuario.Nome);

                mailTemplate.EmailCadastro(senha);

                _mail.Enviar(usuario.Email, mailTemplate.Assunto, mailTemplate.CorpoEmail);
            }
        }

        public Usuario Buscar(int id)
        {
            var usuario=_repo.BuscaPorId(id);
            if (usuario == null)
                throw new Exception("Dados do usuário não encontrado.");
            return usuario;
        }

        public Usuario Autenticar(string email, string senha)
        {
            var usuario = _repo.BuscaPorEmail(email);
            if(usuario==null)
                throw new Exception(Erros.UsuarioInvalido);

            if (usuario.GetStatus() == EUsuarioStatus.Novo)
            {
                if (usuario.Autenticar(senha))
                {
                    _contaApp.SalvarContaPadrao(usuario.Id);
                    _categoriaApp.SalvarCategoriasPadrao(usuario.Id);
                    _formaApp.FormaPadrao(usuario.Id);
                    

                    _repo.Alterar(usuario);
                }
            }
            else
            {
                if (usuario.Autenticar(senha))
                {
                    _repo.Alterar(usuario);
                }
            }
            
         
            return usuario;
        }

        public void Cancelar(int id)
        {
            var usuario = _repo.BuscaPorId(id);
            usuario.Cancelar();
            _repo.Alterar(usuario);
        }

        public void ResetarSenha(string email)
        {
            var usuario = _repo.BuscaPorEmail(email);
            
            if(usuario==null)
                throw new Exception(Erros.EmailInvalido);

            var senha = usuario.GerarSenha();

            _repo.Alterar(usuario);

            var mailTemplate = new MailTemplate(email, usuario.Nome);
            
            mailTemplate.EmailResetarSenha(senha);

            _mail.Enviar(email,mailTemplate.Assunto,mailTemplate.CorpoEmail);
        }

        public void Dispose()
        {
            _repo.Dispose();
            _mail.Dispose();
        }
    }
}
