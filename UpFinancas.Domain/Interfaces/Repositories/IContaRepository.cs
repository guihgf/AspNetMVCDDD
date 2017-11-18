using System.Collections.Generic;
using UpFinancas.Domain.Entities;

namespace UpFinancas.Domain.Interfaces.Repositories
{
    public interface IContaRepository
    {
        IEnumerable<Conta> Listar(int usuario_id);
        Conta BuscarPorId(int id, int usuario_id);
        Conta buscarContaPadrao(int usuario_id, int contaPadraoAtualId);
        void Salvar(Conta conta);
        void Alterar(Conta conta);
        void Excluir(Conta conta);
        void Dispose();
    }
}
