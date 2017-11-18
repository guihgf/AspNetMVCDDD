
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UpFinancas.Domain.Entities;
using UpFinancas.Domain.Interfaces.Repositories;
using UpFinancas.Infra.Data.Context;

namespace UpFinancas.Infra.Data.Repositories
{
    public class UsuarioRepository:IUsuarioRepository,IDisposable
    {
        private readonly UpFinancasContext _context;

        public UsuarioRepository(UpFinancasContext context)
        {
            _context = context;
        }

        public IEnumerable<Usuario> Listar()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario BuscaPorEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public Usuario BuscaPorId(int id)
        {
            var usuario= _context.Usuarios.Find(id);
            _context.Entry(usuario).State = EntityState.Detached;
            return usuario;
        }

        public void Salvar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void Alterar(Usuario usuario)
        {
            _context.Entry(usuario).State=EntityState.Modified;
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
