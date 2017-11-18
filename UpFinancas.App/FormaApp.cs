
using System;
using System.Collections.Generic;
using System.Linq;
using UpFinancas.App.Interfaces;
using UpFinancas.Domain.Entities;
using UpFinancas.Domain.Interfaces.Repositories;

namespace UpFinancas.App
{
    public class FormaApp : IFormaApp
    {
        private readonly IFormaRepository _repo;

        public FormaApp(IFormaRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Forma> Listar(int usuario_id)
        {
            var formas = _repo.Listar(usuario_id);
            return formas;
        }

        public void Salvar(Forma forma)
        {
            if (forma.Id>0)
                _repo.Alterar(forma);
            else
                _repo.Salvar(forma);
        }

        public void AlterarStatus(int id, int usuario_id)
        {
            var forma = _repo.BuscarPorId(id, usuario_id);
            forma.AlterarStatus();
            _repo.Alterar(forma);
        }

        public void Excluir(int id, int usuario_id)
        {
            var forma=_repo.BuscarPorId(id, usuario_id);
            _repo.Excluir(forma);
        }


        public Forma Buscar(int usuarioId,int formaId)
        {
            var pessoa=_repo.BuscarPorId(formaId, usuarioId);
            if(pessoa==null)
                throw new Exception("Forma não encontrada!");
            return pessoa;
        }

        public IEnumerable<Forma> ListarSomenteAtivas(int usuario_id)
        {
            var formas = Listar(usuario_id).Where(p => p.DataDesativacao == null).OrderBy(p=>p.Nome);

            return formas;

        }

        public void FormaPadrao(int usuarioId)
        {
            var forma = new Forma("Dinheiro", usuarioId);
            Salvar(forma);

            forma = new Forma("Cheque", usuarioId);
            Salvar(forma);

            forma = new Forma("Cartão de Crédito", usuarioId);
            Salvar(forma);

            forma = new Forma("Cartão de Débito", usuarioId);
            Salvar(forma);
        }

        public void Dispose()
        {
            _repo.Dispose();
        }
    }
}
