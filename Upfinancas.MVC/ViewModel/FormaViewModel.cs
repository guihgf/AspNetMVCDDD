using System;
using System.ComponentModel.DataAnnotations;

namespace Upfinancas.MVC.ViewModel
{
    public class FormaViewModel
    {
        public int? Id { get; set; }

        [Display(Name ="Nome")]
        [Required(ErrorMessage = "Digite o nome da forma.")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Nome deve possuir entre 5 e 150 caracteres.")]
        public string Nome { get; set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime? DataDesativacao { get; private set; }

        public string GetStatus()
        {
            return DataDesativacao == null ? "Ativo" : "Inativo";
        }

        public string GetDataCadastroPtBr()
        {
            return DataCadastro.ToString("dd/MM/yyyy");
        }
    }
}