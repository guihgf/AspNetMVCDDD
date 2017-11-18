using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpFinancas.Domain.Entities;
using UpFinancas.Domain.Interfaces.Repositories;
using UpFinancas.Infra.Data.Context;

namespace UpFinancas.Infra.Data.Repositories
{
    public class PessoaRepository:IPessoaRepository, IDisposable
    {
        private readonly UpFinancasContext _context;

        public PessoaRepository(UpFinancasContext context)
        {
            _context = context;
        }

        public void Alterar(Pessoa pessoa)
        {
            _context.Entry(pessoa).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Pessoa BuscarPorId(int id, int usuario_id)
        {
            return _context.Pessoas.FirstOrDefault(c => c.UsuarioId == usuario_id && c.Id == id);
        }

        public void Excluir(Pessoa pessoa)
        {
            _context.Pessoas.Remove(pessoa);
            _context.SaveChanges();
        }

        public IEnumerable<Pessoa> Listar(int usuario_id)
        {
            return _context.Pessoas.Where(c => c.UsuarioId == usuario_id).ToList();
        }

        public void Salvar(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}