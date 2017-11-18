using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UpFinancas.Domain.Entities;
using UpFinancas.Domain.Interfaces.Repositories;
using UpFinancas.Infra.Data.Context;

namespace UpFinancas.Infra.Data.Repositories
{
    public class CategoriaRepository:ICategoriaRepository, IDisposable
    {
        private readonly UpFinancasContext _context;

        public CategoriaRepository(UpFinancasContext context)
        {
            _context = context;
        }

        public void Alterar(Categoria categoria)
        {
            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Categoria BuscarPorId(int id, int usuario_id)
        {
            return _context.Categorias.FirstOrDefault(c => c.UsuarioId == usuario_id && c.Id == id);
        }

        public void Excluir(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
        }

        public IEnumerable<Categoria> Listar(int usuario_id)
        {
            return _context.Categorias.Where(c => c.UsuarioId == usuario_id).ToList();
        }

        public void Salvar(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}