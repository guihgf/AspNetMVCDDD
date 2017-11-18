using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using UpFinancas.Domain.Entities;

namespace Upfinancas.MVC.ViewModel
{
    public class EventoViewModel
    {
        public EventoViewModel() {
            MontarListaTipos();
            DataLancamento = DateTime.Now;
        }

        public EventoViewModel(IEnumerable<Conta> contas, IEnumerable<Forma> formas,IEnumerable<Pessoa>pessoas=null, IEnumerable<Categoria> categorias = null)
        {
            MontarListaContas(contas);
            MontarListaFormas(formas);
            MontarListaPessoas(pessoas);
            MontarListaCategorias(categorias);
            MontarListaTipos();
            DataLancamento = DateTime.Now;
        }
        public int? Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do evento.")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Evento deve possuir entre 5 e 150 caracteres.")]
        public string Descricao { get; set; }

        [Range((int)ETipoEvento.Despesa, (int)ETipoEvento.Receita, ErrorMessage = "Tipo de evento inválido.")]
        public int Tipo { get; set; }

        [Required(ErrorMessage = "Digite o valor do evento.")]
        [Range(0.01, Double.MaxValue, ErrorMessage = "Valor deve ser maior que 0 e/ou válido")]
        //[DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency, ErrorMessage ="{0} não é um valor válido.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Valor deve ser no mínimo 0.")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Valor deve ser no mínimo 0.")]
        [DataType(DataType.Currency, ErrorMessage = "{0} não é um valor válido.")]
        public decimal ValorJuros { get; set; }

        [Required(ErrorMessage = "Valor deve ser no mínimo 0.")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Valor deve ser no mínimo 0.")]
        [DataType(DataType.Currency, ErrorMessage = "{0} não é um valor válido.")]
        public decimal ValorDesconto { get; set; }

        [Required(ErrorMessage = "Informe a data de lançamento.")]
        [DataType(DataType.Date,ErrorMessage ="Formato de data inválido.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DataLancamento { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Formato de data inválido.")]
        [Foolproof.RequiredIf("Tipo", 1, ErrorMessage = "Informe a data de vencimento.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DataVencimento { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Formato de data inválido.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DataPagamento { get; set; }

        [StringLength(300, MinimumLength = 5, ErrorMessage = "Observação deve possuir entre 5 e 300 caracteres.")]
        public string Observacao { get; set; }

        [Required(ErrorMessage = "Informe uma conta.")]
        public int ContaId { get; set; }

        [Required(ErrorMessage = "Informe uma forma de pagamento/recebimento.")]
        public int FormaId { get; set; }

        public IEnumerable<SelectListItem> Contas { get; private set; }


        public IEnumerable<SelectListItem> Formas { get; private set; }

        public IEnumerable<SelectListItem> Tipos { get; private set; }

        public int? PessoaId { get; set; }

        public IEnumerable<SelectListItem> Pessoas { get; private set; }

        public int? CategoriaId { get; set; }

        public IEnumerable<SelectListItem> Categorias { get; private set; }

        public string GetTipo()
        {
            return Tipo == (int)ETipoEvento.Despesa ? "Despesas" : "Receitas";
        }

        public void MontarListaContas(IEnumerable<Conta> contas, int idSelecionado=0)
        {
            Contas=new SelectList(contas, "Id", "Nome", idSelecionado==0? Enumerable.FirstOrDefault(contas).Id:idSelecionado);
        }

        public void MontarListaFormas(IEnumerable<Forma> formas, int idSelecionado = 0)
        {
            Formas = new SelectList(formas, "Id", "Nome", idSelecionado == 0 ? Enumerable.FirstOrDefault(formas).Id : idSelecionado);
        }

        public void MontarListaPessoas(IEnumerable<Pessoa> pessoas, int idSelecionado = 0)
        {
            if (pessoas.Any())
            {
                if (idSelecionado > 0)
                    Pessoas = new SelectList(pessoas, "Id", "Nome", idSelecionado);
                else
                    Pessoas = new SelectList(pessoas, "Id", "Nome");
            }
                
        }

        public void MontarListaCategorias(IEnumerable<Categoria> categorias, int idSelecionado = 0)
        {
            if (categorias.Any())
            {
                if (idSelecionado > 0)
                    Categorias = new SelectList(categorias, "Id", "Nome", idSelecionado);
                else
                    Categorias = new SelectList(categorias, "Id", "Nome");
            }

        }

        public void MontarListaTipos(int idSelecionado = 0)
        {
            Tipos = new List<SelectListItem>(){
                    new SelectListItem() {Text="Despesa", Value=((int)ETipoEvento.Despesa).ToString()},
                    new SelectListItem() {Text="Receita", Value=((int)ETipoEvento.Receita).ToString()}
            };
        }

        public string GetDataLancamentoPtBr()
        {
            return DataLancamento.ToString("dd/MM/yyyy");
        }

        public string GetDataVencimentoPtBr()
        {
            return DataVencimento.HasValue ? Convert.ToDateTime(DataVencimento).ToString("dd/MM/yyyy") : "";
        }

        public string GetDataPagamentoPtBr()
        {
            return DataPagamento.HasValue ? Convert.ToDateTime(DataPagamento).ToString("dd/MM/yyyy") : "";
        }
    }
}