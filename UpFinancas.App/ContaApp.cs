
using System;
using System.Collections.Generic;
using System.Linq;
using UpFinancas.App.Interfaces;
using UpFinancas.Domain.Entities;
using UpFinancas.Domain.Interfaces.Repositories;

namespace UpFinancas.App
{
    public class ContaApp : IContaApp
    {
        private readonly IContaRepository _repo;

        public ContaApp(IContaRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Conta> Listar(int usuario_id)
        {
            var contas = _repo.Listar(usuario_id);
            foreach(var conta in contas)
            {
                conta.CalcularSaldo();
            }
            return contas;
        }

        public void Salvar(Conta conta)
        {
            if (conta.Id>0)
                _repo.Alterar(conta);
            else
                _repo.Salvar(conta);

            VerificarContaPadrao(conta);
        }

        public void AlterarStatus(int id, int usuario_id)
        {
            var conta = _repo.BuscarPorId(id, usuario_id);
            conta.AlterarStatus();
            _repo.Alterar(conta);
        }

        public void Excluir(int id, int usuario_id)
        {
            var conta=_repo.BuscarPorId(id, usuario_id);
            _repo.Excluir(conta);
        }

        public void VerificarContaPadrao(Conta conta)
        {
            if (conta.Padrao == (int)EPadraoConta.Sim)
            {
                var contaPadrao=_repo.buscarContaPadrao(conta.UsuarioId,conta.Id);
                if (contaPadrao!=null && (contaPadrao.Id!=conta.Id))
                {
                    contaPadrao.Padrao = (int)EPadraoConta.Não;
                    _repo.Alterar(contaPadrao);
                }                
            }
                
        }

        public Conta Buscar(int usuarioId,int contaId)
        {
            var conta=_repo.BuscarPorId(contaId, usuarioId);
            if(conta==null)
                throw new Exception("Conta não encontrada!");
            return conta;
        }

        public void Dispose()
        {
            _repo.Dispose();
        }

        public IEnumerable<Conta> ListarSomenteAtivas(int usuario_id)
        {
           var contas= Listar(usuario_id).Where(p => p.DataDesativacao == null).OrderByDescending(p=>p.Padrao);

           if (contas.Count() == 0)
                throw new Exception("Não existem contas para realizar o lançamento.");

            return contas;

        }

        public void SalvarContaPadrao(int usuarioId)
        {
            Salvar(new Conta("Conta Corrente", (int)EPadraoConta.Sim, usuarioId));
        }
    }
}
