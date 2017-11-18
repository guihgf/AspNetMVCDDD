using System.Collections.Generic;
using UpFinancas.Domain.Entities;

namespace UpFinancas.Domain.Interfaces.Repositories
{
    public interface IPessoaRepository
    {
        IEnumerable<Pessoa> Listar(int usuario_id);
        Pessoa BuscarPorId(int id, int usuario_id);
        void Salvar(Pessoa pessoa);
        void Alterar(Pessoa pessoa);
        void Excluir(Pessoa pessoa);
        void Dispose();
    }
}
