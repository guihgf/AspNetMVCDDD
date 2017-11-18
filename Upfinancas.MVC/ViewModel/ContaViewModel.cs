using System;
using System.ComponentModel.DataAnnotations;
using UpFinancas.Domain.Entities;

namespace Upfinancas.MVC.ViewModel
{
    public class ContaViewModel
    {
        public int? Id { get; set; }
        [Display(Name ="Nome")]
        [Required(ErrorMessage = "Digite o nome da conta.")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Nome deve possuir entre 5 e 150 caracteres.")]
        public string Nome { get; set; }

        public DateTime DataCadastro { get; private set; }
        public DateTime? DataDesativacao { get; private set; }
        public bool PadraoCheck
        {
            get { return Padrao == 2; }
            set { Padrao = value ? 2 : 1; }
        }
        public int Padrao { get; set; }
        public int EPadraConta { get; private set; }
        [DataType(DataType.Currency)]
        public decimal Saldo { get; set; }

        public string GetPadrao()
        {
            return Padrao==(int)EPadraoConta.Sim? "Sim" : "Não";
        }

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