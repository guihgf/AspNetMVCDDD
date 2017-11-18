using UpFinancas.Domain.Entities;

namespace UpFinancas.App.Interfaces
{
    public interface IUsuarioApp
    {
        void Salvar(Usuario usuario);
        Usuario Buscar(int id);
        Usuario Autenticar(string email, string senha);
        void Cancelar(int id);
        void ResetarSenha(string email);
        void Dispose();

    }
}