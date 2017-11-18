using System;
using System.Collections.Generic;
using UpFinancas.Util;

namespace UpFinancas.Domain.Entities
{
    public class Categoria
    {
        public Categoria(string nome, int tipo, int usuarioId)
        {
            Validations.AssertArgumentLength(nome, 5, 150, "Categoria deve possuir entre 5 e 150 caracteres.");
            Tipo = tipo;
            Nome = nome;
            UsuarioId = usuarioId;

        }
 
        protected Categoria() { }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public int Tipo { get; set; }
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
