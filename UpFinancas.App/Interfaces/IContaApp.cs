using System.Collections.Generic;
using UpFinancas.Domain.Entities;

namespace UpFinancas.App.Interfaces
{
    public interface IContaApp
    {
        IEnumerable<Conta> Listar(int usuario_id);
        Conta Buscar(int usuarioId,int contaId);
        void Salvar(Conta conta);
        void Excluir(int id, int usuario_id);
        void AlterarStatus(int id, int usuario_id);
        void VerificarContaPadrao(Conta conta);
        void Dispose();
        IEnumerable<Conta> ListarSomenteAtivas(int usuario_id);
        void SalvarContaPadrao(int usuarioId);
    }
}
