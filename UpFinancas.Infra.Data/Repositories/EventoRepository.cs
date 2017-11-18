using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UpFinancas.Domain.Entities;
using UpFinancas.Domain.Interfaces.Repositories;
using UpFinancas.Infra.Data.Context;

namespace UpFinancas.Infra.Data.Repositories
{
    public class EventoRepository : IEventoRepository, IDisposable
    {
        private readonly UpFinancasContext _context;

        public EventoRepository(UpFinancasContext context)
        {
            _context = context;
        }

        public void Alterar(Evento evento)
        {
            _context.Entry(evento).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Evento BuscarPorId(int id, int usuario_id)
        {
            return _context.Eventos.FirstOrDefault(c=>c.Conta.UsuarioId==usuario_id && c.Id==id);
        }
        public void Excluir(Evento evento)
        {
            _context.Eventos.Remove(evento);
            _context.SaveChanges();
        }

        public IEnumerable<Evento> Listar(int usuario_id)
        {
            return _context.Eventos.Where(c => c.Conta.UsuarioId == usuario_id).ToList();
        }

        public void Salvar(Evento evento)
        {
            _context.Eventos.Add(evento);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
