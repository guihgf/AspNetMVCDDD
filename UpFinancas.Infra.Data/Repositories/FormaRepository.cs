using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UpFinancas.Domain.Entities;
using UpFinancas.Domain.Interfaces.Repositories;
using UpFinancas.Infra.Data.Context;

namespace UpFinancas.Infra.Data.Repositories
{
    public class FormaRepository:IFormaRepository, IDisposable
    {
        private readonly UpFinancasContext _context;

        public FormaRepository(UpFinancasContext context)
        {
            _context = context;
        }

        public void Alterar(Forma forma)
        {
            _context.Entry(forma).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Forma BuscarPorId(int id, int usuario_id)
        {
            return _context.Formas.FirstOrDefault(c => c.UsuarioId == usuario_id && c.Id == id);
        }

        public void Excluir(Forma forma)
        {
            _context.Formas.Remove(forma);
            _context.SaveChanges();
        }

        public IEnumerable<Forma> Listar(int usuario_id)
        {
            return _context.Formas.Where(c => c.UsuarioId == usuario_id).ToList();
        }

        public void Salvar(Forma forma)
        {
            _context.Formas.Add(forma);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}