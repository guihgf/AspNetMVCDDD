using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using UpFinancas.Domain.Entities;
using Foolproof;

namespace Upfinancas.MVC.ViewModel
{
    public class TransferenciaViewModel
    {
        public TransferenciaViewModel() { }
        public TransferenciaViewModel(IEnumerable<Conta> contas)
        {
            MontarListaContas(contas);
            DataLancamento = DateTime.Now;
        }
        
        public IEnumerable<SelectListItem> ContasEntrada { get; private set; }

        public IEnumerable<SelectListItem> ContasSaida { get; private set; }

        [Required(ErrorMessage = "Informe uma conta.")]
        [NotEqualTo("ContaSaidaId", ErrorMessage = "Conta de entrada deve ser diferente da de saída.")]
        public int ContaEntradaId { get; set; }

        [Required(ErrorMessage = "Informe uma conta.")]
        public int ContaSaidaId { get; set; }

        [Required(ErrorMessage = "Informe a data de lançamento.")]
        [DataType(DataType.Date, ErrorMessage = "Formato de data inválido.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DataLancamento { get; set; }

        [Required(ErrorMessage = "Digite o valor do transferência.")]
        [Range(0.01, Double.MaxValue, ErrorMessage = "Valor deve ser maior que 0 e/ou válido")]
        [DataType(DataType.Currency, ErrorMessage = "{0} não é um valor válido.")]
        public decimal Valor { get; set; }

        [StringLength(300, MinimumLength = 5, ErrorMessage = "Observação deve possuir entre 5 e 300 caracteres.")]
        public string Observacao { get; set; }


        public void MontarListaContas(IEnumerable<Conta> contas, int idSelecionado = 0)
        {
            ContasSaida = new SelectList(contas, "Id", "Nome", idSelecionado == 0 ? Enumerable.FirstOrDefault(contas).Id : idSelecionado);
            ContasEntrada=ContasSaida;
        }
    }
}