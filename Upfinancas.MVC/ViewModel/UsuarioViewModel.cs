using Foolproof;
using System;
using System.ComponentModel.DataAnnotations;

namespace Upfinancas.MVC.ViewModel
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o seu nome.")]
        [StringLength(150,MinimumLength = 5,ErrorMessage = "Nome deve possuir entre 5 e 150 caracteres.")]
        [Display(Name = "Nome:")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o seu e-mail.")]
        [Display(Name ="E-mail:")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [RequiredIf("Id", true, ErrorMessage = "Digite a senha.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha:")]
        public string Senha { get; set; }

        [RequiredIf("Id",true,ErrorMessage = "Confirme a senha.")]
        [DataType(DataType.Password)]
        [Compare("Senha",ErrorMessage = "A confirmação da senha não é a mesma que a senha.")]
        [Display(Name = "Confirme a senha:")]
        public string ConfirmarSenha { get; set; }

        [RequiredIf("Id", false, ErrorMessage = "Digite a soma dos dois números.")]
        [Display(Name = "Qual a soma de:")]
        public string Captcha { get; set; }

        [RequiredIf("Id", false, ErrorMessage = "Confirme o seu e-mail.")]
        [Display(Name = "Confirme seu E-mail:")]
        [DataType(DataType.EmailAddress)]
        [Compare("Email", ErrorMessage = "E-mails são diferentes, verifique.")]
        public string ConfirmeEmail { get; set; }
    }
}