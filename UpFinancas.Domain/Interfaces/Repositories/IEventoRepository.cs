using System.Collections.Generic;
using UpFinancas.Domain.Entities;

namespace UpFinancas.Domain.Interfaces.Repositories
{
    public interface IEventoRepository
    {
        IEnumerable<Evento> Listar(int usuario_id);
        Evento BuscarPorId(int id, int usuario_id);
        void Salvar(Evento evento);
        void Alterar(Evento evento);
        void Excluir(Evento evento);
        void Dispose();
    }
}
