using System.Collections.Generic;
using UpFinancas.Domain.Entities;

namespace UpFinancas.App.Interfaces
{
    public interface IEventoApp
    {
        IEnumerable<Evento> Listar(int usuario_id);
        IEnumerable<Evento> ListarPorConta(int usuario_id, int contaId);
        Evento Buscar(int usuarioId,int eventoId);
        void Salvar(Evento evento);
        void Excluir(int id, int usuario_id);
        void Dispose();
        
    }
}
