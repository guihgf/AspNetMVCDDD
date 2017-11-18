using System;
using System.ComponentModel.DataAnnotations;

namespace Upfinancas.MVC.ViewModel
{
    public class PessoaViewModel
    {
        public int? Id { get; set; }

        [Display(Name ="Nome")]
        [Required(ErrorMessage = "Digite o nome da pessoa.")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Nome deve possuir entre 5 e 150 caracteres.")]
        public string Nome { get; set; }
        [StringLength(30, ErrorMessage = "Telefone deve possuir no máximo 30 caracteres.")]
        public string Telefone { get; set; }
        [StringLength(30, ErrorMessage = "Celular deve possuir no máximo 30 caracteres.")]
        public string Celular { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Digite um e-mail válido")]
        [StringLength(150, ErrorMessage = "E-mail deve possuir no máximo 150 caracteres.")]
        public string Email { get; set; }
        [StringLength(150, ErrorMessage = "Rua deve possuir no máximo 150 caracteres.")]
        public string Rua { get; set; }
        [StringLength(20, ErrorMessage = "Número deve possuir no máximo 20 caracteres.")]
        public string Numero { get; set; }
        [StringLength(150, ErrorMessage = "Bairro deve possuir no máximo 150 caracteres.")]
        public string Bairro { get; set; }
        [StringLength(150, ErrorMessage = "Cidade deve possuir no máximo 150 caracteres.")]
        public string Cidade { get; set; }
        [StringLength(200, ErrorMessage = "Complemento deve possuir no máximo 200 caracteres.")]
        public string Complemento { get; set; }
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