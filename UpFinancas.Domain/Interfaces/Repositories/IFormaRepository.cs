using System.Collections.Generic;
using UpFinancas.Domain.Entities;

namespace UpFinancas.Domain.Interfaces.Repositories
{
    public interface IFormaRepository
    {
        IEnumerable<Forma> Listar(int usuario_id);
        Forma BuscarPorId(int id, int usuario_id);
        void Salvar(Forma categoria);
        void Alterar(Forma categoria);
        void Excluir(Forma categoria);
        void Dispose();
    }
}
