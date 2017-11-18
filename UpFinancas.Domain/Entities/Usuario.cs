using System;
using System.Collections.Generic;
using UpFinancas.Util;

namespace UpFinancas.Domain.Entities
{
    public class Usuario
    {
        #region Ctor

        public Usuario(string nome, string email,string confirmarEmail)
        {
            Validations.AssertArgumentLength(nome,5,150,Erros.NomeUsuarioInvalido);

            EmailValidation.AssertIsValid(email);

            Validations.AssertArgumentEquals(email,confirmarEmail,Erros.ConfirmarEmail);

            Nome = nome;

            Email = email;

            DataCadastro = DateTime.Now;

        }

        protected Usuario() { }
        #endregion

        public int Id { get; set; }

        public string Nome { get; private set; }
        
        public string Email { get; private set; }
        
        public string Senha { get; set; }
        
        public DateTime DataCadastro { get; private set; }
        
        public DateTime? DataCancelamento { get; private set; }
        
        public DateTime? DataReativacao { get; private set; }

        public DateTime? DataConfirmacao { get; set; }

        public virtual ICollection<Conta> Contas { get; set; }

        public virtual ICollection<Pessoa> Pessoas { get; set; }

        public virtual ICollection<Categoria> Categorias { get; set; }

        public virtual ICollection<Forma> Formas { get; set; }

        public EUsuarioStatus GetStatus()
        {
            if (DataCancelamento != null)
                return EUsuarioStatus.Inativo;
            else if (DataConfirmacao == null)
                return EUsuarioStatus.Novo;
            else
                return EUsuarioStatus.Ativo;
        }

        public string GerarSenha()
        {
            var senha = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = senha;
            EncriptarSenha();
            return senha;
        }

        public bool Autenticar(string senha)
        {
            Validations.AssertArgumentNotEquals(Senha, SenhaValidation.Encrypt(senha), Erros.UsuarioInvalido);

            if (GetStatus() == EUsuarioStatus.Inativo)
            {
                 Reativar();
                return true;
            }

            if (GetStatus() == EUsuarioStatus.Novo)
            {
                ConfirmarCadastro();
                return true;
            }

            return false;
        }

        public void ConfirmarCadastro()
        {
            DataConfirmacao = DateTime.Now;

        }
        public void Reativar()
        {
            if (GetStatus() != EUsuarioStatus.Inativo) return;

            DataCancelamento = null;

            DataReativacao = DateTime.Now;
        }

        public void Cancelar()
        {
            if (GetStatus() != EUsuarioStatus.Ativo) return;

            DataCancelamento = DateTime.Now;
        }

        public void AlterarSenha(string senha, string confirmarSenha)
        {
            Validations.AssertArgumentEquals(senha,confirmarSenha,Erros.ConfirmacaoSenhaInvalida);
        }

        public void EncriptarSenha()
        {
            Senha = SenhaValidation.Encrypt(Senha);
        }


    }
}
