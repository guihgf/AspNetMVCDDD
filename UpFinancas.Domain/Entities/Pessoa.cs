using System;
using System.Collections.Generic;
using UpFinancas.Util;

namespace UpFinancas.Domain.Entities
{
    public class Pessoa
    {
        public Pessoa(string nome, Usuario usuario)
        {
            Validations.AssertArgumentLength(nome, 5, 150, "Nome deve possuir entre 5 e 150 caracteres.");
            Nome = nome;
            UsuarioId = usuario.Id;

        }
 
        protected Pessoa() { }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Complemento { get; set; }
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
