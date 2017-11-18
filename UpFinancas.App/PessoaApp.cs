
using System;
using System.Collections.Generic;
using System.Linq;
using UpFinancas.App.Interfaces;
using UpFinancas.Domain.Entities;
using UpFinancas.Domain.Interfaces.Repositories;

namespace UpFinancas.App
{
    public class PessoaApp : IPessoaApp
    {
        private readonly IPessoaRepository _repo;

        public PessoaApp(IPessoaRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Pessoa> Listar(int usuario_id)
        {
            var pessoas = _repo.Listar(usuario_id);
            return pessoas;
        }

        public void Salvar(Pessoa pessoa)
        {
            if (pessoa.Id>0)
                _repo.Alterar(pessoa);
            else
                _repo.Salvar(pessoa);
        }

        public void AlterarStatus(int id, int usuario_id)
        {
            var pessoa = _repo.BuscarPorId(id, usuario_id);
            pessoa.AlterarStatus();
            _repo.Alterar(pessoa);
        }

        public void Excluir(int id, int usuario_id)
        {
            var pessoa=_repo.BuscarPorId(id, usuario_id);
            _repo.Excluir(pessoa);
        }


        public Pessoa Buscar(int usuarioId,int pessoaId)
        {
            var pessoa=_repo.BuscarPorId(pessoaId, usuarioId);
            if(pessoa==null)
                throw new Exception("Pessoa não encontrada!");
            return pessoa;
        }

        public IEnumerable<Pessoa> ListarSomenteAtivas(int usuario_id)
        {
            var pessoas = Listar(usuario_id).Where(p => p.DataDesativacao == null).OrderBy(p=>p.Nome);

            return pessoas;

        }

        public void Dispose()
        {
            _repo.Dispose();
        }
    }
}
