using System;
using System.Collections.Generic;
using UpFinancas.Util;
namespace UpFinancas.Domain.Entities
{
    public class Conta
    {
        public Conta(string nome, int padrao,int usuarioId)
        {
            Validations.AssertArgumentLength(nome,5,150,Erros.ContaInvalida);
            Nome = nome;
            DataCadastro = DateTime.Now;
            Padrao = padrao;
            UsuarioId = usuarioId;

        }
        protected Conta() {
        }

        public int Id { get; private set; }
        public string Nome { get; set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime? DataDesativacao { get; private set; }
        public int Padrao { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; private set; }
        public virtual ICollection<Evento>Eventos { get; set; }
        public virtual double Saldo { get; private set; }

        public void CalcularSaldo()
        {
            foreach(var evento in Eventos)
            {
                if (evento.DataPagamento != null)
                {
                    if (evento.Tipo == (int)ETipoEvento.Despesa)
                        Saldo = Saldo - evento.Valor;
                    else
                        Saldo = Saldo + evento.Valor;
                }
                
            }
        }

        public void AlterarStatus()
        {
            if (DataDesativacao == null)
                DataDesativacao = DateTime.Now;
            else
                DataDesativacao = null;
        }

    }
}
