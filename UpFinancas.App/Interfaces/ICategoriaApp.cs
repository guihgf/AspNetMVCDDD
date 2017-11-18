using System.Collections.Generic;
using UpFinancas.Domain.Entities;

namespace UpFinancas.App.Interfaces
{
    public interface ICategoriaApp
    {
        IEnumerable<Categoria> Listar(int usuario_id);
        Categoria Buscar(int usuarioId,int pessoaId);
        void Salvar(Categoria pessoa);
        void Excluir(int id, int usuario_id);
        void AlterarStatus(int id, int usuario_id);
        IEnumerable<Categoria> ListarSomenteAtivasPorTipo(int usuario_id, int tipo);
        void SalvarCategoriasPadrao(int usuarioId);
        void Dispose();
    }
}
