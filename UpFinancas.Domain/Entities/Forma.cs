using System;
using System.Collections.Generic;
using UpFinancas.Util;

namespace UpFinancas.Domain.Entities
{
    public class Forma
    {
        public Forma(string nome,int usuarioId)
        {
            Validations.AssertArgumentLength(nome, 5, 150, "Forma deve possuir entre 5 e 150 caracteres.");
            Nome = nome;
            UsuarioId = usuarioId;

        }
 
        protected Forma() { }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime? DataDesativacao { get; private set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; private set; }
        public virtual ICollection<Evento> Eventos { get; set; }

        public void AlterarStatus()
        {
            if (DataDesativacao == null)
                DataDesativacao = DateTime.Now;
            else
                DataDesativacao = null;
        }
    }
}
