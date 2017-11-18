using System.Collections.Generic;
using UpFinancas.Domain.Entities;

namespace UpFinancas.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> Listar();
        Usuario BuscaPorEmail(string email);
        Usuario BuscaPorId(int id);
        void Salvar(Usuario usuario);
        void Alterar(Usuario usuario);
        void Dispose();

    }
}