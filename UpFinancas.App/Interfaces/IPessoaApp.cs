using System.Collections.Generic;
using UpFinancas.Domain.Entities;

namespace UpFinancas.App.Interfaces
{
    public interface IPessoaApp
    {
        IEnumerable<Pessoa> Listar(int usuario_id);
        Pessoa Buscar(int usuarioId,int pessoaId);
        void Salvar(Pessoa pessoa);
        void Excluir(int id, int usuario_id);
        void AlterarStatus(int id, int usuario_id);
        IEnumerable<Pessoa> ListarSomenteAtivas(int usuario_id);
        void Dispose();
    }
}
