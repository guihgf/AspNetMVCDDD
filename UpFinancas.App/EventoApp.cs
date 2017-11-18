
using System;
using System.Collections.Generic;
using System.Linq;
using UpFinancas.App.Interfaces;
using UpFinancas.Domain.Entities;
using UpFinancas.Domain.Interfaces.Repositories;

namespace UpFinancas.App
{
    public class EventoApp : IEventoApp
    {
        private readonly IEventoRepository _repo;

        public EventoApp(IEventoRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Evento> Listar(int usuario_id)
        {
            var eventos = _repo.Listar(usuario_id);
            return eventos;
        }

        public IEnumerable<Evento> ListarPorConta(int usuario_id, int contaId)
        {
            var eventos = _repo.Listar(usuario_id);
            return eventos.Where(e=>e.ContaId==contaId).ToList();
        }

        public void Salvar(Evento evento)
        {
            if (evento.Id>0)
                _repo.Alterar(evento);
            else
                _repo.Salvar(evento);
        }

        public void Excluir(int id, int usuario_id)
        {
            var evento=_repo.BuscarPorId(id, usuario_id);
            _repo.Excluir(evento);
        }

        public Evento Buscar(int usuarioId,int eventoId)
        {
            var evento=_repo.BuscarPorId(eventoId, usuarioId);
            if(evento==null)
                throw new Exception("Evento não encontrado!");
            return evento;
        }

        public void Dispose()
        {
            _repo.Dispose();
        }

        
    }
}
