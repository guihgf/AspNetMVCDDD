using System.Collections.Generic;
using UpFinancas.Domain.Entities;

namespace UpFinancas.Domain.Interfaces.Repositories
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Listar(int usuario_id);
        Categoria BuscarPorId(int id, int usuario_id);
        void Salvar(Categoria categoria);
        void Alterar(Categoria categoria);
        void Excluir(Categoria categoria);
        void Dispose();
    }
}
