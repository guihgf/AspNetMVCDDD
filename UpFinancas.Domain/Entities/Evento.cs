using System;
using UpFinancas.Util;

namespace UpFinancas.Domain.Entities
{
    public class Evento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Tipo { get; set; }
        public double Valor { get; set; }
        public double ValorJuros { get; set; }
        public double ValorDesconto { get; set; }
        public string Observacao { get; set; }
        public DateTime DataLancamento { get; set; }
        public DateTime? DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public int ContaId { get; set; }
        public virtual Conta Conta { get; private set; }
        public int? PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; private set; }
        public int? CategoriaId { get; set; }
        public virtual Categoria Categoria { get; private set; }
        public int FormaId { get; set; }
        public virtual Forma Forma { get; private set; }

        protected Evento()
        {
        }

        public Evento(string descricao, int tipo, double valor, double valorJuros, double valorDesconto,DateTime dataLancamento, DateTime dataVencimento, DateTime dataPagamento,int contaId, string observacao)
        {
            
            Descricao = descricao;
            Tipo = tipo;
            Valor = valor;
            DataLancamento = dataLancamento;
            ContaId = contaId;
            ValorJuros = valorJuros;
            ValorDesconto = valorDesconto;
            DataVencimento = dataVencimento;
            DataPagamento = dataPagamento;
            Observacao = observacao;

            if (Tipo == (int)ETipoEvento.Despesa && !DataVencimento.HasValue)
                throw new Exception("Informe a data de vencimento da despesa.");

            if(!(Valor>0))
                throw  new Exception("Valor deve ser maior que 0.");

            Validations.AssertArgumentLength(Descricao,5,150,"Evento deve possuir entre 5 e 150 caracteres");
            Validations.AssertArgumentLength(Descricao, 300, "Observação deve possuir no máximo 300 caracteres");
        }
    }
}
