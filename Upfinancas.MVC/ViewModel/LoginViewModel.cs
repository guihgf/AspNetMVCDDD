using System.ComponentModel.DataAnnotations;

namespace Upfinancas.MVC.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name ="E-mail")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Digite um e-mail válido")]
        [Required(ErrorMessage ="Digite o e-mail.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage="Digite a senha.")]
        public string Senha { get; set; }
    }
}