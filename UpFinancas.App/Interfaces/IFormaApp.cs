using System.Collections.Generic;
using UpFinancas.Domain.Entities;

namespace UpFinancas.App.Interfaces
{
    public interface IFormaApp
    {
        IEnumerable<Forma> Listar(int usuario_id);
        Forma Buscar(int usuarioId,int formaId);
        void Salvar(Forma Forma);
        void Excluir(int id, int usuario_id);
        void AlterarStatus(int id, int usuario_id);
        IEnumerable<Forma> ListarSomenteAtivas(int usuario_id);
        void FormaPadrao(int usuarioId);
        void Dispose();
    }
}
